using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivoPagamentoJudiciais.Services.Selenium.Adquira;
using VivoPagamentoJudiciais.Services.Selenium.API;
using VivoPagamentoJudiciais.Services.Selenium.VivoPagto;
using VivoPagamentoJudiciais.Utility;

namespace VivoPagamentoJudiciais.Services
{
    public class ScheduleService : IScheduleService
    {

        private readonly IVivoPagamentoService _vivoPagamento;
        private readonly IAdquiraService _adquira;
        private readonly IAPIService _API;

        private readonly ILogger<ScheduleService> _logger;

        public ScheduleService(IVivoPagamentoService vivoPagamento,
                               IAdquiraService adquira,
                               IAPIService API,
                               ILogger<ScheduleService> logger)
        {

            _vivoPagamento = vivoPagamento;
            _API = API;
            _adquira = adquira;
            _logger = logger;
        }

        public void Startup(int AppId)
        {
            _logger.LogInformation(string.Format(LogsProcess.MethodExecute, Variables.ScheduleStartup));

            try
            {
                if (AppId == 1)
                    _adquira.Startup();
                else if (AppId == 2)
                    _vivoPagamento.Startup();
                else if (AppId == 3)
                    _API.Startup();


            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, string.Format(LogsProcess.ErrorMethod, Variables.ScheduleStartup));
                throw ex;
            }
        }
    }
}
