using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VivoPagamentoJudiciais.Services.Core;
using VivoPagamentoJudiciais.Utility;

namespace VivoPagamentoJudiciais.Services.Selenium.API
{
    public class APIService : IAPIService
    {
        private readonly ILogger<APIService> _logger;

        public APIService(
           ILogger<APIService> logger)
        {
            _logger = logger;
        }

        public void Startup()
        {
            Stopwatch watch;
            long elapsedMs = 0;

            try
            {
                _logger.LogInformation(string.Format(LogsProcess.InitProcess, Variables.API, DateTime.Now.ToString()));

                watch = Stopwatch.StartNew();
                APIManagment objAPI = new APIManagment();

                string[] lstDocuments = Directory.GetFiles("FaturaInternaFiles");

                if (lstDocuments.Length > 0)
                {
                    Console.WriteLine("\r\n");
                    Console.WriteLine("\r\n");
                    Console.WriteLine("Document ID Response:");
                    Console.WriteLine("\r\n");
                }


                foreach (string document in lstDocuments)
                {
                    FileInfo file = new FileInfo(document);
                    objAPI.CreateNewDocument(FileParse.ToBase64String(file.FullName));

                }

                watch.Stop();

                elapsedMs = watch.ElapsedMilliseconds;

                _logger.LogInformation(string.Format(LogsProcess.FinishProcess, Variables.API, DateTime.Now.ToString()));
                _logger.LogInformation(string.Format(LogsProcess.TimeExecution, Variables.API, elapsedMs.ToString()));

                Thread.Sleep(10000);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format(LogsProcess.ErrorMethod, Variables.APIServiceStartup));

                throw ex;
            }
           
        }
    }
}
