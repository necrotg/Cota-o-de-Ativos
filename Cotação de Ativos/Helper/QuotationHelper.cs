using Cotação_de_Ativos.Models;
using Cotação_de_Ativos.Utils;
using NLog;
using NLog.Fluent;
using System.Net;
using System.Net.Http.Headers;

public class QuotationHelper
{
    static ILogger logger = LogManager.GetCurrentClassLogger();
    public static Boolean IsInvalidArguments(String[] args)
    {
        if (args.Length < 3 || args.Length > 3)
        {
            logger.Error(ErrorMessages.INVALID_ARGUMENTS);
            logger.Error(ErrorMessages.INSERT_ARGUMENTS);
            logger.Error(ErrorMessages.ARGUMENTS_EXEMPLE);
            return true;
        }
        try
        {
            Convert.ToDouble(args[1]);
        }
        catch (Exception ex)
        {
            logger.Error(ErrorMessages.SELLING_PRICE_INVALID);
            logger.Error(ex.ToString());
            return true;
        }

        try
        {
            Convert.ToDouble(args[2]);
        }
        catch (Exception ex)
        {
            logger.Error(ErrorMessages.BUYING_PRICE_INVALID);
            logger.Error(ex.ToString());
            return true;
        }

        return false;
    }
    public static ErrorCode ValidateResponseStatus(HttpResponseMessage response)
    {
        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                logger.Info(Constants.SUCCESS_REQUEST_MESSAGE);
                return ErrorCode.Success;
            case HttpStatusCode.NotFound:
                logger.Error(ErrorMessages.INVALID_ASSET);
                return ErrorCode.Failure;
            case HttpStatusCode.Unauthorized:
                logger.Error(ErrorMessages.INVALID_TOKEN);
                return ErrorCode.Failure;
            case HttpStatusCode.PaymentRequired:
                logger.Error(ErrorMessages.REQUEST_LIMIT_EXCEEDED);
                return ErrorCode.Failure;
            case HttpStatusCode.InternalServerError:
                logger.Error(ErrorMessages.EXTERNAL_QUOTE_SERVICE_UNAVAILABLE);
                return ErrorCode.Failure;
            default:
                logger.Error(ErrorMessages.UNMAPPED_STATUS_CODE);
                return ErrorCode.Failure;
        }
    }

    internal static ErrorCode ValidateExternalResponse(ExternalQuotationResponse quotationResponse)
    {
        if (quotationResponse == null || quotationResponse.Results.Length == 0 || quotationResponse.Results[0].HistoricalDataPrice.Length == 0 || quotationResponse.Results[0].HistoricalDataPrice[0].Close == 0.0)
        {
            logger.Info(ErrorMessages.INVALID_RESPONSE);
            return ErrorCode.Failure;
        }

        return ErrorCode.Success;
    }

    internal static Boolean IsToSendEmail(Double oldPrice, ExternalQuotationResponse quotationResponse)
    {
        if (oldPrice != 0.0 && oldPrice == getAssetPrice(quotationResponse))
        {
            logger.Info(Constants.ASSET_HAS_SAME_VALUE);
            return false;
        }
        return true;
    }

    internal static void InitHttpClient(HttpClient client)
    {
        client.BaseAddress = new Uri(Constants.URL_EXTERNAL_SERVICE);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.MEDIATYPE_JSON));
    }
    internal static Double getAssetPrice(ExternalQuotationResponse quotationResponse)
    {
        HistoricalDataPrice[] historicalDataPrice = quotationResponse.Results[0].HistoricalDataPrice;
        HistoricalDataPrice newestHistoricalDataPrice = new HistoricalDataPrice();
        if (historicalDataPrice.Length > 1)
        {
            long newestDate = 0l;
            for (int i = 0; i < historicalDataPrice.Length; i++)
            {
                long date = historicalDataPrice[i].Date;
                if (date > newestDate)
                {
                    newestHistoricalDataPrice = historicalDataPrice[i];
                    newestDate = date;
                }
            }
        }
        if (newestHistoricalDataPrice == null)
        {
            return historicalDataPrice[0].Close;
        }
        return newestHistoricalDataPrice.Close;
    }
}


