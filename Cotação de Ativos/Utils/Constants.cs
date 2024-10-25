namespace Cotação_de_Ativos.Utils
{
    public static class Constants
    {
        public const string URL_EXTERNAL_SERVICE = "https://brapi.dev/";
        public const string MEDIATYPE_JSON = "application/json";
        public const string SUCCESS_REQUEST_MESSAGE = "Ativo recebido de servico externo com succeso";
        public const string SELLING_EMAIL_MESSAGE_BODY = "Preço da acao maior que o preco de Venda, é recomendado Vender a ação";
        public const string BUYING_EMAIL_MESSAGE_BODY = "Preço da acao menor que o preco de Compra, é recomendado Comprar a açAo";
        public const string ENDPOINT_QUOTE_API = "api/quote/";
        public const string PARAMETERS_QUOTE_API = "?range=1d&interval=1d&token=b2GiVqcS42a3KdDhKU5wDu";
        public const string EMAIL_SUBJECT = "Monitoramento de Cotação";
        public const string EMAIL_DISPLT_NAME = "Alerta de Cotação";
        public const string SUCCESS_EMAIL_SENT = "Email Enviado com Sucesso";
        public const string ASSET_HAS_SAME_VALUE = "Preço do Ativo não alterado, não será necessário enviar E-mail";
    }
}
