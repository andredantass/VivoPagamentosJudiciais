using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using VivoPagamentoJudiciais.Model.Entities;
using VivoPagamentoJudiciais.Model.Enum;
using VivoPagamentoJudiciais.Utilities.File;

namespace VivoPagamentoJudiciais.Utilities.DynamicClass
{
    public class DynamicExpressions
    {
        public static dynamic FileInClass(MemoryStream ms, dynamic @class, GeracaoArquivo _parameters)
        {
            dynamic file = null;

            switch ((ETipoArquivo)_parameters.IdTipoArquivo)
            {
                case ETipoArquivo.CSV:
                    file = MappingCSV.CsvToClass(@class, ms, _parameters.Header);
                    break;
                case ETipoArquivo.XLS:
                   //implementar caso precise
                    break;
                case ETipoArquivo.XLSX:
                    //implementar caso precise
                    break;
                case ETipoArquivo.TXT:
                    //implementar caso precise
                    break;
                default:
                    break;
            }

            return file;
        }

        public static IEnumerable<string> DataGenerateWithExpression<T>(string propertyName, ICollection<T> fileInClass) where T : class
        {
            try
            {
                var propertyInfos = typeof(T).GetProperties();

                var property = propertyInfos.Where(x => x.Name == propertyName).FirstOrDefault();

                var paramExpr = Expression.Parameter(typeof(T), "r");
                var propertyExpr = Expression.Property(paramExpr, property);
                var expression = Expression.Lambda<Func<T, string>>(propertyExpr, paramExpr);

                return fileInClass.Select(expression.Compile());

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
