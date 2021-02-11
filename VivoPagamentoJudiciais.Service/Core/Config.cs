using System;
using System.Collections.Generic;
using System.Text;

namespace VivoPagamentoJudiciais.Service.Core
{
    public static class Config
    {
        public static class API
        {
            public static string URL = "http://apiocr-uat.interfile.com.br:8085";
        }
        public static class AdquiraPage
        {
            public static string AdquiraLoginURL = "https://mkpes.adquira.com/marketplace/login.jsp";
            public static string AdquiraFaturaInternaURL = "https://mkpes.adquira.com/marketplace/control/observer/internalInvoices";

            public static class FaturaInternaForm
            {
                public static string Tomador = "TBRA Telefonica Brasil S/A [02.558.157/0001-62]";
                public static string SiglaAreaGestora = "U8";
                public static string TipoDocumentoSAP = "KN - Depósitos Judiciais Juridico";
                public static string AreaReclamacao = "U8 - DIV CONSUMIDOR SP E RJ";
                public static string TipoNotaFiscalAnaliseTributaria = "Não se aplica (NA)";
                public static string Empresa = "TBRA-ES";
                public static string FormaPagamento = "Deposito judicial";
                public static string TipoPagamento = "Cível";
                public static string DescricaoPagamento = "Sentença – Obrigação de Pagar";

                public static string NumeroFaturaInterna = "XXXXXXXXXX";
                public static string DataVencimento = DateTime.Now.ToString("dd/MM/yyyy");
                public static string NumeroDocumentoSuporte = "XXXXXXXXXX";
                public static string NumeroSequencial = "XXXXXXXXXX";
                public static string NomeFavorecido = "XXXXXXXXXX";
                public static string CPFCNPJFavorecido = "XXXXXXXXXX";
                public static string CodigoSAPFavorecido = "XXXXXXXXXX";
                public static string CodigoSAPFornecedor = "XXXXXXXXXX";
                public static string Comarca = "XXXXXXXXXX";
                public static string CodigoOcorrencia = "XXXXXXXXXX";

                public static string NomeBanco = "XXXXXXXXXX";
                public static string NumeroBanco = "XXXXXXXXXX";
                public static string NumeroSAP = "XXXXXXXXXX";
                public static string Agencia = "XXXXXXXXXX";
                public static string ContaCorrente = "XXXXXXXXXX";
                public static string NumeroID = "XXXXXXXXXX";

            }
            public static class Credentials
            {
                public static string UserName = "Tiago_Machado";
                public static string Password = "Tiago@123456";
            }
        }
        public static class VivoPagtoPage
        {
            public static string VivoPagtoLoginURL = "https://telefonicabpo-dpi.azurewebsites.net/Account/Login";
            public static string VivoPagtoFaturaInternaURL = "https://telefonicabpo-dpi.azurewebsites.net/FaturaInterna/Incluir";
            public static class Credentials
            {
                public static string UserName = "28751446855";
                public static string Password = "Atento@2021";
            }

            public static class FaturaInternaForm
            {
                public static string NomeModelo = "T6 - JJJ#AA-GER SR GESTAO DE CONTENC ESPECIAL/AC";
                public static string AreaReclamacao = "U8-DIV CONSUMIDOR";
                public static string TipoPagamento = "Custas";
                public static string MesReferencia = DateTime.Now.Month.ToString();
                public static string ValorDocumento = "Automatico";

                public static string Fornecedor = "XXXXXXXXXX";
                public static string Sequencial = "XXXXXXXXXX";
                public static string CodigoOcorrencia = "XXXXXXXXXX";
                public static string DataVencimento = "XXXXXXXXXX";
                public static string UpLoadArquivos = "XXXXXXXXXX";



            }
        }
    }
}
