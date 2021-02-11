using VivoPagamentoJudiciais.Model.Entities;

namespace VivoPagamentoJudiciais.Model.TypeFiles
{
    public class ExcelFile : GeracaoArquivoClasse
    {
        public int IdExcelFileColumn { get; set; }

        public string ColumnPosition { get; set; }

        public string HeaderName { get; set; }

        public bool IsCollection { get; set; }

    }
}
