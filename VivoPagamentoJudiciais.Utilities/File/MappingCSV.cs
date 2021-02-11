using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace VivoPagamentoJudiciais.Utilities.File
{
   public class MappingCSV
    {
        public static dynamic CsvToClass(dynamic @class, MemoryStream file, bool hasHeader = true)
        {
            var csv = typeof(MappingCSV);

            var csvInfo = csv.GetMethod(Variables.Mapping);
            var mappingCsv = csvInfo.MakeGenericMethod((Type)@class.GetType());

            return (dynamic)mappingCsv.Invoke(null, new object[] { file, hasHeader });
        }


        public static IEnumerable<T> Mapping<T>(MemoryStream file, bool hasHeader) where T : class
        {

            using (var memoryStream = new MemoryStream(file.GetBuffer()))
            using (var reader = new StreamReader(memoryStream, Encoding.GetEncoding(Variables.ISO8859_1)))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.Delimiter = ";";
                csv.Configuration.MissingFieldFound = null;
                csv.Configuration.BadDataFound = null;
                csv.Configuration.TrimOptions = TrimOptions.InsideQuotes;
                csv.Configuration.HasHeaderRecord = hasHeader;
                csv.Configuration.ShouldSkipRecord = (record) =>
                {
                    record[0] = record[0].Replace("\0", "");

                    if (record[0].Equals(string.Empty) && record.Length.Equals(1))
                    {
                        return true;
                    }

                    return false;
                };

                return csv.GetRecords<T>().ToList();
            }

        }
    }
}
