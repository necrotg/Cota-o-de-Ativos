using Cotação_de_Ativos.Service;
using NLog;

class QuotatationMonitoring
{

    static HttpClient client = new HttpClient();
    static ILogger logger = LogManager.GetCurrentClassLogger();
    static void Main(String[] args)
    {
        logger.Info("Iniciando Aplicação");

        if (QuotationHelper.IsInvalidArguments(args))
        {
            return;
        }

        QuotationHelper.InitHttpClient(client);

        QuotationMonitoringService.ExecuteQuotationEachTenSeconds(10, args,client);
    }
    
}