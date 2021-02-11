using System;

namespace VivoPagamentoJudiciais.Model.Entities
{
    public class GeracaoArquivoDePara
    {
        public int IdGeracaoArquivoDePara { get; set; }
        public int IdGeracaoArquivo { get; set; }
        public int IdTipoArquivo { get; set; }
        public string CaminhoSaida { get; set; }
        public DateTime DataCadastro { get; set; }
        public TemplateArquivoDePara IdTemplateArquivoDeParaNavigation { get; set; }
    }
}
