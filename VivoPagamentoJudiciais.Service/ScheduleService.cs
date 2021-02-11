using Microsoft.Extensions.Logging;
using System;
using VivoPagamentoJudiciais.Service.Selenium.Adquira;
using VivoPagamentoJudiciais.Service.Selenium.Processum;
using VivoPagamentoJudiciais.Service.Selenium.VivoPagto;
using VivoPagamentoJudiciais.Utilities;

namespace VivoPagamentoJudiciais.Service
{
    public class ScheduleService : IScheduleService
    {
        private readonly IProcessumService _processum;
        private readonly IAdquiraService _adquira;
        private readonly IVivoPagamentoService _vivoPagamento;

        private readonly ILogger<ScheduleService> _logger;

        public ScheduleService(IProcessumService processum,
                               IAdquiraService adquira,
                               IVivoPagamentoService vivoPagamento,
                               ILogger<ScheduleService> logger)
        {
            _processum = processum;
            _adquira = adquira;
            _vivoPagamento = vivoPagamento;
            _logger = logger;
        }

        public void Startup()
        {
            _logger.LogInformation(string.Format(LogsProcess.MethodExecute, Variables.ScheduleStartup));

            try
            {
                //_vivoPagamento.Startup();

                //_adquira.Startup();
                
                //_processum.Startup();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, string.Format(LogsProcess.ErrorMethod, Variables.ScheduleStartup));
                throw ex;
            }
        }
    }
}
