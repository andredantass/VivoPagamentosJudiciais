using VivoPagamentoJudiciais.Model.Entities;

namespace VivoPagamentoJudiciais.Service.DePara
{
    public interface IExcel
    {
        void TreatFile(dynamic fileInClass, dynamic @class, GeracaoArquivo _parameters);
    }
}
