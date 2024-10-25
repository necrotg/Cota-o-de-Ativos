using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cotação_de_Ativos.Utils
{
    public static class ErrorMessages
    {
        public static string INVALID_ASSET = "Ativo inválido";
        public static string INVALID_ARGUMENTS = "Número de parâmetros inválido";
        public static string EMPTY_QUOTE_INFO = "Informaçäo de cotacao está vazia";
        public static string INSERT_ARGUMENTS = "Por favor insira os sequintes parametros: [NomeDoAtivo] [PreçoParaVenda] [PreçoParaCompra]";
        public static string ARGUMENTS_EXEMPLE = "Exemplo: stock-quote-alert.exe PETR4 22.67 22.59";
        public static string SELLING_PRICE_INVALID = "Preço Para Venda inválido, o valor dever conter apenas números\n";
        public static string BUYING_PRICE_INVALID = "Preço Para Compra inválido, o valor dever conter apenas números\n";
        public static string INVALID_TOKEN = "Tonken Inválido";
        public static string REQUEST_LIMIT_EXCEEDED = "Limite de requisições do plano foram atingidos";
        public static string EXTERNAL_QUOTE_SERVICE_UNAVAILABLE = "Servico externo de consulta de cotacao não disponível, tente novamente mais tarde";
        public static string UNMAPPED_STATUS_CODE = "Status Code não mapeado, favor entrar em contato com o Suporte";
        public static string INVALID_RESPONSE = "Response da API externa de cotacao invalida";
    }
}
