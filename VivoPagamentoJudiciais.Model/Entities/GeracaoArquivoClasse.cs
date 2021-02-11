using System;
using VivoPagamentoJudiciais.Model.TypeFiles;

namespace VivoPagamentoJudiciais.Model.Entities
{
    public class GeracaoArquivoClasse
    {
        public int IdGeracaoArquivoClasse { get; set; }
        public int? IdGeracaoArquivo { get; set; }

        private string _nomePropriedade { get; set; }
        public string NomePropriedade { get { return _nomePropriedade; } set { _nomePropriedade = value.ToLower(); } }

        public string TipoPropriedade { get; set; }

        private string _customAttribute;
        public string CustomAttribute { get { return _customAttribute; } set { _customAttribute = value.ToLower(); } }

        public Csv PropridadesCsv { get; set; }

        public DateTime DataCadastro { get; set; }
    }
}
