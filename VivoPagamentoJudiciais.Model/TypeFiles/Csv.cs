using VivoPagamentoJudiciais.Model.Entities;

namespace VivoPagamentoJudiciais.Model.TypeFiles
{
    public class Csv : GeracaoArquivoClasse
    {
        public int IdCsvFileColumn { get; set; }

        public int Index { get; set; }

        public string HeaderName { get; set; }

    }
}
