using Cotação_de_Ativos.Models;
using Cotação_de_Ativos.Utils;
using System.Net.Http.Formatting;
using NLog;

namespace Cotação_de_Ativos.Service
{
    internal class QuotationMonitoringService
    {
        static ILogger logger = LogManager.GetCurrentClassLogger();

        static ErrorCode errorCode = ErrorCode.Success;
        static EmailService emailService = new EmailService();
        static ExternalQuotationResponse quotationResponse = new ExternalQuotationResponse();
        static Boolean isToSendEmail = true;
        private string assetName = null;

        public static void ExecuteQuotationEachTenSeconds(int seconds, String[] args, HttpClient client)
        {
            TimeSpan periodTimeSpan = TimeSpan.FromSeconds(seconds);

            while (errorCode == ErrorCode.Success)
            {

                RunExternalAsyncQuoteService(args,client).GetAwaiter().GetResult();
                if (errorCode == ErrorCode.Failure)
                {
                    return;
                }
                Thread.Sleep(periodTimeSpan);
                GC.Collect();
            }
        }
        static async Task RunExternalAsyncQuoteService(String[] args,HttpClient client)
        {
            String AssetName = args[0];
            quotationResponse = await GetQuotationAsync(Constants.ENDPOINT_QUOTE_API + AssetName + Constants.PARAMETERS_QUOTE_API,client);
            errorCode = VerifyIfBuyOrSell(quotationResponse, args);
            if (errorCode == ErrorCode.Failure)
            {
                return;
            }
        }
        static async Task<ExternalQuotationResponse> GetQuotationAsync(string path,HttpClient client)
        {
            List<MediaTypeFormatter> formatters = new List<MediaTypeFormatter>() { new JsonMediaTypeFormatter() };
            HttpResponseMessage response = await client.GetAsync(path);

            errorCode = QuotationHelper.ValidateResponseStatus(response);
            if (errorCode == ErrorCode.Failure) return null;

            Double oldPrice = 0.0;
            if (quotationResponse.Results != null)
            {
                oldPrice = QuotationHelper.getAssetPrice(quotationResponse);
            }
            
            quotationResponse = await response.Content.ReadAsAsync<ExternalQuotationResponse>(formatters);

            errorCode = QuotationHelper.ValidateExternalResponse(quotationResponse);
            if (errorCode == ErrorCode.Failure) return null;

            isToSendEmail = QuotationHelper.IsToSendEmail(oldPrice,quotationResponse);
            return quotationResponse;
        }


        static ErrorCode VerifyIfBuyOrSell(ExternalQuotationResponse ativo, String[] args)
        {
            Double sellingPrice = Convert.ToDouble(args[1]);
            Double buyingPrice = Convert.ToDouble(args[2]);
            Double assetPrice = ativo.Results[0].HistoricalDataPrice[0].Close;
            try
            {
                if (assetPrice > sellingPrice && isToSendEmail)
                {
                    logger.Info(Constants.SELLING_EMAIL_MESSAGE_BODY);
                    emailService.sendEmail(Constants.SELLING_EMAIL_MESSAGE_BODY,assetPrice,sellingPrice, buyingPrice);
                }
                else if (assetPrice < buyingPrice && isToSendEmail)
                {
                    logger.Info(Constants.BUYING_EMAIL_MESSAGE_BODY);
                    emailService.sendEmail(Constants.BUYING_EMAIL_MESSAGE_BODY, assetPrice, sellingPrice, buyingPrice);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return ErrorCode.Failure;
            }

            return ErrorCode.Success;
        }
    }
}
