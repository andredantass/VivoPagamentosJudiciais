using Microsoft.Extensions.Caching.Memory;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VivoPagamentoJudiciais.Data.Interfaces;
using VivoPagamentoJudiciais.Model.Entities;
using Dapper;
using VivoPagamentoJudiciais.Model.TypeFiles;
using VivoPagamentoJudiciais.Data;
using System.Linq;
using VivoPagamentoJudiciais.Utilities.EmailConnection;
using System.IO;
using VivoPagamentoJudiciais.Utilities.File;
using VivoPagamentoJudiciais.Utilities.DynamicClass;
using VivoPagamentoJudiciais.Model.Enum;
using VivoPagamentoJudiciais.Service.DePara;
using Exchange = Microsoft.Exchange.WebServices.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using VivoPagamentoJudiciais.Utilities;
using VivoPagamentoJudiciais.Service.Selenium.Adquira;
using VivoPagamentoJudiciais.Service.Core;
using VivoPagamentoJudiciais.Service.UIElement.Adquira;

namespace VivoPagamentoJudiciais.Service.Selenium.Adquira
{
    public class AdquiraService : IAdquiraService
    {
        private readonly IRepository _repository;
        private readonly IMemoryCache _cache;
        private readonly IExcel _excel;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AdquiraService> _logger;

        //private GeracaoArquivo _parametersProcessum;

        //private ManualResetEvent resetEvent = new ManualResetEvent(false);

        //private static ChromeDriver driver;
        //private static string expectedFilePath = "";
        //static dynamic @class = null;

        public AdquiraService(
            IRepository repository,
            IMemoryCache cache,
            IExcel excel,
            IConfiguration configuration,
            ILogger<AdquiraService> logger)
        {
            _repository = repository;
            _cache = cache;
            _excel = excel;
            _configuration = configuration;
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
                NavigateTo.AdquiraLoginPage();
                ActionsPage.FillAdquiraLoginForm(Config.AdquiraPage.Credentials.UserName, 
                                                 Config.AdquiraPage.Credentials.Password);

                NavigateTo.AdquiraFaturaInternaPage();

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
                ActionsPage.TearsDownDrive();
            }
        }


    }
}
