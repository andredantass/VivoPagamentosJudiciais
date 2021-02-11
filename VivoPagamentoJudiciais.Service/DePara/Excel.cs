using Dapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VivoPagamentoJudiciais.Data;
using VivoPagamentoJudiciais.Data.Interfaces;
using VivoPagamentoJudiciais.Model.Entities;
using VivoPagamentoJudiciais.Model.TypeFiles;
using VivoPagamentoJudiciais.Utilities;
using VivoPagamentoJudiciais.Utilities.DynamicClass;

namespace VivoPagamentoJudiciais.Service.DePara
{
    public class Excel: IExcel
    {

        private readonly IMemoryCache _cache;
        private readonly IRepository _repository;
        private readonly ILogger<Excel> _logger;

        public Excel(IRepository repository, IMemoryCache cache, ILogger<Excel> logger)
        {
            _repository = repository;
            _cache = cache;
            _logger = logger;
        }
        public async void TreatFile(dynamic fileInClass, dynamic @class, GeracaoArquivo _parameters)
        {
            try
            {
                Stopwatch watch;
                long elapsedMs = 0;

                watch = Stopwatch.StartNew();

                _logger.LogInformation(string.Format(LogsProcess.InitProcess, Variables.ExcelTreatFile, DateTime.Now.ToString()));

                var dynamicExpression = typeof(DynamicExpressions);

                var programInfo = dynamicExpression.GetMethod(Variables.DataGenerateWithExpression);
                var dataGenerateWithExpression = programInfo.MakeGenericMethod((Type)@class.GetType());

                FileStream fileDePara = null;

                var propertyInfos = ((object)@class).GetType().GetProperties();

                var ExcelDeParaProperties = await GetExcelDePara(_parameters.IdGeracaoArquivo);

                if (_parameters.IdGeracaoArquivoDeParaNavigation.IdTemplateArquivoDeParaNavigation != null)
                {
                    //Tem template

                    fileDePara = new FileStream(_parameters.
                        IdGeracaoArquivoDeParaNavigation.
                        IdTemplateArquivoDeParaNavigation.
                        CaminhoArquivo, FileMode.Open, FileAccess.Read);
                }

                using (ExcelPackage package = new ExcelPackage(fileDePara))
                {
                    var ws = package.Workbook.Worksheets.First();

                    if (ExcelDeParaProperties.Where(x => x.IsCollection).Count() > 0)
                    {
                        ws.Cells[2, 1].LoadFromCollection(fileInClass);

                        return;
                    }

                    foreach (var item in ExcelDeParaProperties)
                    {
                        var data = (dynamic)dataGenerateWithExpression.
                            Invoke(null, new object[] { item.NomePropriedade, fileInClass });

                        ws.Cells[item.ColumnPosition].LoadFromCollection(data);
                    }

                    ws.Cells.AutoFitColumns();

                    var nameFile = Guid.NewGuid().ToString().Replace("-", "");

                    FileInfo excelFile = new FileInfo($@"{_parameters.IdGeracaoArquivoDeParaNavigation.CaminhoSaida}\{nameFile}.xlsx");
                    package.SaveAs(excelFile);
                }
                
                watch.Stop();

                elapsedMs = watch.ElapsedMilliseconds;

                _logger.LogInformation(string.Format(LogsProcess.FinishProcess, Variables.ExcelTreatFile, DateTime.Now.ToString()));
                _logger.LogInformation(string.Format(LogsProcess.TimeExecution, Variables.ExcelTreatFile, elapsedMs.ToString()));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format(LogsProcess.ErrorMethod, Variables.ExcelTreatFile));

                throw ex;
            }
        }

        public async Task<IEnumerable<ExcelFile>> GetExcelDePara(int idGeracaoArquivo)
        {
            try
            {
                Stopwatch watch;
                long elapsedMs = 0;

                watch = Stopwatch.StartNew();

                _logger.LogInformation(string.Format(LogsProcess.InitProcess, Variables.GetExcelDePara, DateTime.Now.ToString()));

                var parameters = new DynamicParameters();
                parameters.Add(Parameters.IdGeracaoArquivo, idGeracaoArquivo);

                watch.Stop();

                elapsedMs = watch.ElapsedMilliseconds;

                _logger.LogInformation(string.Format(LogsProcess.FinishProcess, Variables.GetExcelDePara, DateTime.Now.ToString()));
                _logger.LogInformation(string.Format(LogsProcess.TimeExecution, Variables.GetExcelDePara, elapsedMs.ToString()));

                return await Task.FromResult(_repository.QueryAsync<ExcelFile>(Querys.GetExcelDePara, parameters).Result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
