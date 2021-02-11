using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using VivoPagamentoJudiciais.Data.Interfaces;
using VivoPagamentoJudiciais.Service.Core;
using VivoPagamentoJudiciais.Service.DePara;
using VivoPagamentoJudiciais.Utilities;

namespace VivoPagamentoJudiciais.Service.Selenium.VivoPagto
{
    public class VivoPagamentoService : IVivoPagamentoService
    {
        private readonly IRepository _repository;
        private readonly IMemoryCache _cache;
        private readonly IExcel _excel;
        private readonly IConfiguration _configuration;
        private readonly ILogger<VivoPagamentoService> _logger;

        public VivoPagamentoService(IRepository repository,
            IMemoryCache cache,
            IExcel excel,
            IConfiguration configuration,
            ILogger<VivoPagamentoService> logger)
        {
            _repository = repository;
            _cache = cache;
            _excel = excel;
            _configuration = configuration;
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

                ActionsPage.FillVivoPagamentoLoginForm(Config.VivoPagtoPage.Credentials.UserName,
                                                       Config.VivoPagtoPage.Credentials.Password);

                NavigateTo.VivoPagtoFaturaInternaPage();

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
                ActionsPage.TearsDownDrive();
            }
        }
    }
}
