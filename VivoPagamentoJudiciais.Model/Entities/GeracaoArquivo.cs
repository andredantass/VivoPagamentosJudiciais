using System;
using System.Collections.Generic;

namespace VivoPagamentoJudiciais.Model.Entities
{
    public class GeracaoArquivo
    {
        public GeracaoArquivo()
        {
            GeracaoArquivoClasse = new HashSet<GeracaoArquivoClasse>();
        }

        public int IdGeracaoArquivo { get; set; }
        public int IdTipoArquivo { get; set; }
        public bool Zip { get; set; }
        public bool Header { get; set; }
        public string Descricao { get; set; }
        public string NomeArquivo { get; set; }
        public string CaminhoArquivo { get; set; }
        public DateTime DataCadastro { get; set; }
        public virtual TipoArquivo IdTipoArquivoNavigation { get; set; }
        public virtual GeracaoArquivoDePara IdGeracaoArquivoDeParaNavigation { get; set; }
        public virtual ICollection<GeracaoArquivoClasse> GeracaoArquivoClasse { get; set; }
        public virtual Email GeracaoArquivoEmail { get; set; }

    }
}
