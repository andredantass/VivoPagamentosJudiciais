
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VivoPagamentoJudiciais.Services.Core;
using VivoPagamentoJudiciais.Utility;

namespace VivoPagamentoJudiciais.Services.Selenium.VivoPagto
{
    public class VivoPagamentoService : IVivoPagamentoService
    {
        //private readonly IRepository _repository;
        //private readonly IMemoryCache _cache;
        //private readonly IExcel _excel;
        //private readonly IConfiguration _configuration;
        private readonly ILogger<VivoPagamentoService> _logger;

        public VivoPagamentoService(
            ILogger<VivoPagamentoService> logger)
        {
           
            _logger = logger;
        }

        public void Startup()
        {
            Stopwatch watch;
            long elapsedMs = 0;

            try
            {
                _logger.LogInformation(string.Format(LogsProcess.InitProcess, Variables.VivoPagamento, DateTime.Now.ToString()));
                watch = Stopwatch.StartNew();

                ActionsPage.InitializeDriver();
                NavigateTo.VivoPagtoPage();
                Thread.Sleep(3000);
                ActionsPage.FillVivoPagamentoLoginForm(Config.VivoPagtoPage.Credentials.UserName,
                                                       Config.VivoPagtoPage.Credentials.Password);
                Thread.Sleep(3000);
                NavigateTo.VivoPagtoFaturaInternaPage();
                Thread.Sleep(3000);

                List<string> lstExtractedDate = FileParse.ExtractValuesExcelSheet("Planilha/Planilha Automação - Manual Fundo Fixo.xlsx");

                ActionsPage.FillVivoPagamentoFaturaInternaForm();

                watch.Stop();

                elapsedMs = watch.ElapsedMilliseconds;

                _logger.LogInformation(string.Format(LogsProcess.FinishProcess, Variables.VivoPagamento, DateTime.Now.ToString()));
                _logger.LogInformation(string.Format(LogsProcess.TimeExecution, Variables.VivoPagamento, elapsedMs.ToString()));

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, string.Format(LogsProcess.ErrorMethod, Variables.VivoPagamentoServiceStartup));
            }
            finally
            {
                Driver.driver.Quit();
            }
        }
    }
}
