using System;

namespace VivoPagamentoJudiciais.Model.Entities
{
    public class Email
    { 
        public int IdEmail { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public DateTime DataCadastro { get; set; }
        public string EnviadoPor { get; set; }
        public int TempoStreaming { get; set; }
    }
}
