
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VivoPagamentoJudiciais.Services.Core;
using VivoPagamentoJudiciais.Services.UIElement.Adquira;
using VivoPagamentoJudiciais.Utility;

namespace VivoPagamentoJudiciais.Services.Selenium.Adquira
{
    public class AdquiraService : IAdquiraService
    {

        private readonly ILogger<AdquiraService> _logger;

        //private GeracaoArquivo _parametersProcessum;

        //private ManualResetEvent resetEvent = new ManualResetEvent(false);

        //private static ChromeDriver driver;
        //private static string expectedFilePath = "";
        //static dynamic @class = null;

        public AdquiraService(
            ILogger<AdquiraService> logger)
        {
            _logger = logger;
        }

        public async void Startup()
        {
            Stopwatch watch;
            long elapsedMs = 0;

            try
            {
                _logger.LogInformation(string.Format(LogsProcess.InitProcess, Variables.Adquira, DateTime.Now.ToString()));

                watch = Stopwatch.StartNew();
                //APIManagment objAPI = new APIManagment();
                //objAPI.CreateNewDocument();
                AdquiraFaturaInternaPage objFaturaInterna = new AdquiraFaturaInternaPage();
                
                ActionsPage.InitializeDriver();
                Thread.Sleep(3000);
                NavigateTo.AdquiraLoginPage();
                Thread.Sleep(3000);
                ActionsPage.FillAdquiraLoginForm(Config.AdquiraPage.Credentials.UserName, 
                                                 Config.AdquiraPage.Credentials.Password);
                Thread.Sleep(3000);
                NavigateTo.AdquiraFaturaInternaPage();
                Thread.Sleep(3000);
                objFaturaInterna.btnCriarFatura.Click();
                ActionsPage.FillAdquiraFaturaInternaForm();
                watch.Stop();

                elapsedMs = watch.ElapsedMilliseconds;

                _logger.LogInformation(string.Format(LogsProcess.FinishProcess, Variables.Adquira, DateTime.Now.ToString()));
                _logger.LogInformation(string.Format(LogsProcess.TimeExecution, Variables.Adquira, elapsedMs.ToString()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format(LogsProcess.ErrorMethod, Variables.AdquiraServiceStartup));

                throw ex;
            }
            finally 
            {
                Driver.driver.Quit();
            }
        }


    }
}
