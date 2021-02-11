using System;

namespace VivoPagamentoJudiciais.Model.Entities
{
    public class TemplateArquivoDePara
    {
        public int IdTemplateArquivoDePara { get; set; }

        public int IdGeracaoArquivoDePara { get; set; }

        public string CaminhoArquivo { get; set; }

        public DateTime DataCadastro { get; set; }
    }
}
