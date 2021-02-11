namespace VivoPagamentoJudiciais.Model.Entities
{
    public class GeracaoArquivoEmail
    {
        public int IdGeracaoArquivoEmail { get; set; }
        public int IdEmail { get; set; }
        public int IdGeracaoArquivo { get; set; }
        public virtual Email IdEmailNavigation { get; set; }
        public virtual GeracaoArquivo IdGeracaoArquivoNavigation { get; set; }
    }
}
