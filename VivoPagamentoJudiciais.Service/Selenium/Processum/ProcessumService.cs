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

namespace VivoPagamentoJudiciais.Service.Selenium.Processum
{
    public class ProcessumService : IProcessumService
    {
        private readonly IRepository _repository;
        private readonly IMemoryCache _cache;
        private readonly IExcel _excel;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProcessumService> _logger;

        private GeracaoArquivo _parametersProcessum;

        private ManualResetEvent resetEvent = new ManualResetEvent(false);

        private static ChromeDriver driver;
        private static string expectedFilePath = "";
        static dynamic @class = null;

        public ProcessumService(
            IRepository repository,
            IMemoryCache cache,
            IExcel excel,
            IConfiguration configuration,
            ILogger<ProcessumService> logger)
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
                _logger.LogInformation(string.Format(LogsProcess.InitProcess, Variables.Processum, DateTime.Now.ToString()));

                watch = Stopwatch.StartNew();

                var chromeOptions = new ChromeOptions();

                _parametersProcessum = await GetGeracaoArquivo(Convert.ToInt32(_configuration.GetSection("AppConfiguration")["IdGeracaoArquivo"]));

                @class = _cache.GetOrCreate(_parametersProcessum.Descricao, x =>
                {
                    x.SetPriority(CacheItemPriority.NeverRemove);

                    return ConstructClass.CreateNewObject(_parametersProcessum);
                });

                chromeOptions.AddUserProfilePreference("download.default_directory", _parametersProcessum.CaminhoArquivo);

                expectedFilePath = string.Concat(_parametersProcessum.CaminhoArquivo, @"\", _parametersProcessum.NomeArquivo);

                using (driver = new ChromeDriver(_configuration.GetSection("Selenium")["ChromeDriver"], chromeOptions))
                {
                    driver.Navigate().GoToUrl(_configuration.GetSection("AppConfiguration")["UrlSite"]);

                    LoginSite();
                    GenerateFile();
                    StreamingExchange();

                    var fileExists = DowloadFile();

                    if (fileExists)
                    {
                        TreatFile();
                    }

                    Cancellation();
                }

                watch.Stop();

                elapsedMs = watch.ElapsedMilliseconds;

                _logger.LogInformation(string.Format(LogsProcess.FinishProcess, Variables.Processum, DateTime.Now.ToString()));
                _logger.LogInformation(string.Format(LogsProcess.TimeExecution, Variables.Processum, elapsedMs.ToString()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format(LogsProcess.ErrorMethod, Variables.ProcessumServiceStartup));

                throw ex;
            }
        }

        public void LoginSite()
        {
            try
            {
                Stopwatch watch;
                long elapsedMs = 0;

                watch = Stopwatch.StartNew();

                _logger.LogInformation(string.Format(LogsProcess.InitProcess, Variables.LoginSite, DateTime.Now.ToString()));

                var login = driver.FindElement(By.Name("username"));
                new Actions(driver).MoveToElement(login).Click().Perform();
                new Actions(driver).MoveToElement(login).SendKeys(_configuration.GetSection("AppConfiguration")["LoginSite"]).Perform();

                var senha = driver.FindElement(By.Name("password"));
                new Actions(driver).MoveToElement(senha).Click().Perform();
                new Actions(driver).MoveToElement(senha).SendKeys(_configuration.GetSection("AppConfiguration")["SenhaSite"]).Perform();

                var buttonLogin = driver.FindElement(By.XPath("//*[@id='form']/div[3]/div/input"));
                buttonLogin.Click();

                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;

                _logger.LogInformation(string.Format(LogsProcess.FinishProcess, Variables.LoginSite, DateTime.Now.ToString()));
                _logger.LogInformation(string.Format(LogsProcess.TimeExecution, Variables.LoginSite, elapsedMs.ToString()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GenerateFile()
        {
            try
            {
                Stopwatch watch;
                long elapsedMs = 0;

                watch = Stopwatch.StartNew();

                _logger.LogInformation(string.Format(LogsProcess.InitProcess, Variables.GenerateFile, DateTime.Now.ToString()));

                var daysLate = 1;
                var dayNow = DateTime.Now.DayOfWeek;

                if ((dayNow == DayOfWeek.Saturday) || (dayNow == DayOfWeek.Sunday))
                {
                    daysLate = 3;
                }

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

                var report = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='formMenu__idJsp3_menu']/table/tbody/tr/td[2]")));
                new Actions(driver).MoveToElement(report).Perform();

                var process = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='cmSubMenuID3']/table/tbody/tr[1]")));
                new Actions(driver).MoveToElement(process).Perform();

                var processMonitorig = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='cmSubMenuID4']/table/tbody/tr[1]/td[2]")));
                new Actions(driver).MoveToElement(processMonitorig).Click().Perform();

                var dropdownSituation = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("fPesquisa:situacao")));
                new SelectElement(dropdownSituation).SelectByText("Pendente");

                var dropdownTypePeriod = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("fPesquisa:tipoData")));
                new SelectElement(dropdownTypePeriod).SelectByText("Cadastro Ocorrencia");

                var startDateValue = DateTime.Now.AddDays(-daysLate).ToString("ddMMyyyy");
                var endDateValue = DateTime.Now.ToString("ddMMyyyy");

                var startDate = driver.FindElement(By.Id("fPesquisa:dataInicial"));
                new Actions(driver).MoveToElement(startDate).Click().Perform();
                new Actions(driver).MoveToElement(startDate).SendKeys(startDateValue).Perform();

                var endDate = driver.FindElement(By.Id("fPesquisa:dataFinal"));
                new Actions(driver).MoveToElement(endDate).Click().Perform();
                new Actions(driver).MoveToElement(endDate).SendKeys(endDateValue).Perform();

                var listOption = new List<string>
                {
                    "Obrigações de pagar",
                    "Prestação - Adiant. Fundo Fixo",
                    "Reembolso de despesas"
                };

                foreach (var item in listOption)
                {
                    var clickToSelectGroup = driver.FindElement(By.Id("fPesquisa:_idJsp66"));
                    var optiongroup = driver.FindElement(By.Name("fPesquisa:_idJsp63"));
                    new SelectElement(optiongroup).SelectByText(item);
                    new Actions(driver).MoveToElement(clickToSelectGroup).Click().Perform();
                }

                var email = driver.FindElement(By.Id("fPesquisa:email"));
                new Actions(driver).MoveToElement(email).Click().Perform();
                new Actions(driver).MoveToElement(email).SendKeys(_parametersProcessum.GeracaoArquivoEmail.Login).Perform();

                var generateReport = driver.FindElement(By.Id("btnGerarRelTrab"));
                generateReport.Click();

                elapsedMs = watch.ElapsedMilliseconds;

                _logger.LogInformation(string.Format(LogsProcess.FinishProcess, Variables.GenerateFile, DateTime.Now.ToString()));
                _logger.LogInformation(string.Format(LogsProcess.TimeExecution, Variables.GenerateFile, elapsedMs.ToString()));

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void StreamingExchange()
        {
            try
            {
                Stopwatch watch;
                long elapsedMs = 0;

                watch = Stopwatch.StartNew();

                _logger.LogInformation(string.Format(LogsProcess.InitProcess, Variables.StreamingExchange, DateTime.Now.ToString()));

                Exchange.ExchangeService service = new Exchange.ExchangeService(Exchange.ExchangeVersion.Exchange2013);
                Exchange.WebCredentials wbcred = new Exchange.WebCredentials(
                    _parametersProcessum.GeracaoArquivoEmail.Login,
                    _parametersProcessum.GeracaoArquivoEmail.Senha);

                service.Credentials = wbcred;
                service.AutodiscoverUrl(_parametersProcessum.GeracaoArquivoEmail.Login, EWSConnection.RedirectionUrlValidationCallback);

                EWSConnection.SetStreamingNotifications(service, resetEvent, _parametersProcessum);

                resetEvent.WaitOne();

                watch.Stop();

                elapsedMs = watch.ElapsedMilliseconds;

                _logger.LogInformation(string.Format(LogsProcess.FinishProcess, Variables.StreamingExchange, DateTime.Now.ToString()));
                _logger.LogInformation(string.Format(LogsProcess.TimeExecution, Variables.StreamingExchange, elapsedMs.ToString()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void TreatFile()
        {
            try
            {
                Stopwatch watch;
                long elapsedMs = 0;

                watch = Stopwatch.StartNew();

                _logger.LogInformation(string.Format(LogsProcess.InitProcess, Variables.TreatFile, DateTime.Now.ToString()));

                dynamic fileInClass = null;
                MemoryStream file = null;

                if (_parametersProcessum.Zip)
                {
                    file = ExtractFile.Extract(expectedFilePath);
                }
                else
                {
                    FileStream fs = new FileStream(expectedFilePath, FileMode.Open, FileAccess.Read);
                    file = new MemoryStream();
                    fs.CopyTo(file);
                }

                fileInClass = DynamicExpressions.FileInClass(file, @class, _parametersProcessum);

                if (_parametersProcessum.IdGeracaoArquivoDeParaNavigation != null)
                {
                    //Tem de Para

                    switch ((ETipoArquivo)_parametersProcessum.IdGeracaoArquivoDeParaNavigation.IdTipoArquivo)
                    {
                        case ETipoArquivo.CSV:
                            //implementar caso precise
                            break;

                        case ETipoArquivo.XLSX:
                            _excel.TreatFile(fileInClass, @class, _parametersProcessum);
                            break;

                        case ETipoArquivo.XLS:
                            //implementar caso precise
                            break;

                        case ETipoArquivo.TXT:
                            //implementar caso precise
                            break;
                        default:
                            break;
                    }
                }

                if (File.Exists(expectedFilePath))
                {
                    File.Delete(expectedFilePath);
                }

                watch.Stop();

                elapsedMs = watch.ElapsedMilliseconds;

                _logger.LogInformation(string.Format(LogsProcess.FinishProcess, Variables.TreatFile, DateTime.Now.ToString()));
                _logger.LogInformation(string.Format(LogsProcess.TimeExecution, Variables.TreatFile, elapsedMs.ToString()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DowloadFile()
        {
            try
            {
                Stopwatch watch;
                long elapsedMs = 0;

                watch = Stopwatch.StartNew();

                _logger.LogInformation(string.Format(LogsProcess.InitProcess, Variables.DowloadFile, DateTime.Now.ToString()));

                var solicitationDate = DateTime.Now.ToString("ddMMyyyy");
                var fileExists = false;

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

                var report = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='formMenu__idJsp3_menu']/table/tbody/tr/td[3]")));
                new Actions(driver).MoveToElement(report).Perform();

                var process = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='cmSubMenuID6']/table/tbody/tr[1]/td[2]")));
                new Actions(driver).MoveToElement(process).Perform();

                var processMonitorig = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='cmSubMenuID7']/table/tbody/tr[7]/td[2]")));
                new Actions(driver).MoveToElement(processMonitorig).Click().Perform();

                var startDate = driver.FindElement(By.Id("fPesquisarRelatorio:dataInicial"));
                new Actions(driver).MoveToElement(startDate).Click().Perform();
                new Actions(driver).MoveToElement(startDate).SendKeys(solicitationDate).Perform();

                var endDate = driver.FindElement(By.Id("fPesquisarRelatorio:dataFinal"));
                new Actions(driver).MoveToElement(endDate).Click().Perform();
                new Actions(driver).MoveToElement(endDate).SendKeys(solicitationDate).Perform();

                var buttonSearch = driver.FindElement(By.Id("fPesquisarRelatorio:btnFiltrar"));
                buttonSearch.Click();

                var buttonDownloadExcel = driver.FindElement(By.Id("fPesquisarRelatorio:dtbListaPlanilha:0:_idJsp46"));
                buttonDownloadExcel.Click();

                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(300));
                wait.Until(x => fileExists = File.Exists(expectedFilePath));

                elapsedMs = watch.ElapsedMilliseconds;

                _logger.LogInformation(string.Format(LogsProcess.FinishProcess, Variables.DowloadFile, DateTime.Now.ToString()));
                _logger.LogInformation(string.Format(LogsProcess.TimeExecution, Variables.DowloadFile, elapsedMs.ToString()));

                return fileExists;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Cancellation()
        {
            Stopwatch watch;
            long elapsedMs = 0;

            try
            {
                _logger.LogInformation(string.Format(LogsProcess.InitProcess, Variables.Processum, DateTime.Now.ToString()));
                watch = Stopwatch.StartNew();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

                var menu = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/table[1]/tbody/tr[2]/td/form/table/tbody/tr/td/div/table/tbody/tr/td[1]")));
                new Actions(driver).MoveToElement(menu).Perform();

                var process = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/table[1]/tbody/tr[2]/td/form/table/tbody/tr/td/div/div[1]/table/tbody/tr[1]/td[2]")));
                new Actions(driver).MoveToElement(process).Click().Perform();

                var sequencialNumber = driver.FindElement(By.Id("fPesquisa:sequencial"));
                new Actions(driver).MoveToElement(sequencialNumber).Click().Perform();
                new Actions(driver).MoveToElement(sequencialNumber).SendKeys("25125/2019--148").Perform();

                var search = driver.FindElement(By.Id("fPesquisa:lblBtnFiltrar"));
                search.Click();

                var sequencialElement = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("fPesquisa:dtbProcesso:0:_idJsp30")));
                new Actions(driver).MoveToElement(sequencialElement).Click().Perform();

                var monitoring = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("fDetalhar:btAcompanhamento")));
                new Actions(driver).MoveToElement(monitoring).Click().Perform();

                watch.Stop();

                elapsedMs = watch.ElapsedMilliseconds;

                _logger.LogInformation(string.Format(LogsProcess.FinishProcess, Variables.Processum, DateTime.Now.ToString()));
                _logger.LogInformation(string.Format(LogsProcess.TimeExecution, Variables.Processum, elapsedMs.ToString()));

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<GeracaoArquivo> GetGeracaoArquivo(int idGeracaoArquivo)
        {
            try
            {
                Stopwatch watch;
                long elapsedMs = 0;

                watch = Stopwatch.StartNew();

                _logger.LogInformation(string.Format(LogsProcess.InitProcess, Variables.GetGeracaoArquivo, DateTime.Now.ToString()));

                var parameters = new DynamicParameters();
                parameters.Add(Parameters.IdGeracaoArquivo, idGeracaoArquivo);

                Func<GeracaoArquivo,
                    TipoArquivo,
                    GeracaoArquivoDePara,
                    TemplateArquivoDePara,
                    Email,
                    GeracaoArquivo> map = (geracaoArquivo, tipoArquivo, geracaoArquivoDepara, templateArquivoDePara, email) =>
                    {
                        geracaoArquivo.IdTipoArquivoNavigation = tipoArquivo;
                        geracaoArquivo.IdGeracaoArquivoDeParaNavigation = geracaoArquivoDepara;
                        geracaoArquivo.IdGeracaoArquivoDeParaNavigation.IdTemplateArquivoDeParaNavigation = templateArquivoDePara;
                        geracaoArquivo.GeracaoArquivoEmail = email;

                        return geracaoArquivo;
                    };

                var result = await Task.FromResult(_repository.QueryAsync(Querys.GetGeracaoArquivo, map, Parameters.SplitGetGeracaoArquivo, parameters).Result.FirstOrDefault());

                result.GeracaoArquivoClasse = GetGeracaoArquivoClasse(idGeracaoArquivo).Result.AsList();

                watch.Stop();

                elapsedMs = watch.ElapsedMilliseconds;

                _logger.LogInformation(string.Format(LogsProcess.FinishProcess, Variables.GetGeracaoArquivo, DateTime.Now.ToString()));
                _logger.LogInformation(string.Format(LogsProcess.TimeExecution, Variables.GetGeracaoArquivo, elapsedMs.ToString()));

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<GeracaoArquivoClasse>> GetGeracaoArquivoClasse(int idGeracaoArquivo)
        {
            try
            {
                Stopwatch watch;
                long elapsedMs = 0;

                watch = Stopwatch.StartNew();

                _logger.LogInformation(string.Format(LogsProcess.InitProcess, Variables.GetGeracaoArquivoClasse, DateTime.Now.ToString()));


                var parameters = new DynamicParameters();
                parameters.Add(Parameters.IdGeracaoArquivo, idGeracaoArquivo);

                Func<GeracaoArquivoClasse, Csv, GeracaoArquivoClasse> map = (geracaoArquivoClasse, csvFile) =>
                {
                    geracaoArquivoClasse.PropridadesCsv = csvFile;

                    return geracaoArquivoClasse;
                };

                watch.Stop();

                elapsedMs = watch.ElapsedMilliseconds;

                _logger.LogInformation(string.Format(LogsProcess.FinishProcess, Variables.GetGeracaoArquivoClasse, DateTime.Now.ToString()));
                _logger.LogInformation(string.Format(LogsProcess.TimeExecution, Variables.GetGeracaoArquivoClasse, elapsedMs.ToString()));

                return await Task.FromResult(_repository.QueryAsync(Querys.GetGeracaoArquivoClasse, map, Parameters.SplitGetGeracaoArquivoClasse, parameters).Result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
