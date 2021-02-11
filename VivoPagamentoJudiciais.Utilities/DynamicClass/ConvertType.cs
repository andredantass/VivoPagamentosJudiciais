using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VivoPagamentoJudiciais.Utilities.DynamicClass
{
    public class ConvertType
    {
        private Dictionary<string, Type> keyValues = new Dictionary<string, Type>();

        public ConvertType()
        {
            keyValues.Add("string", typeof(string));
            keyValues.Add("datetime", typeof(DateTime));
            keyValues.Add("nulldatetime", typeof(DateTime?));
            keyValues.Add("bool", typeof(bool));
            keyValues.Add("short", typeof(short));
            keyValues.Add("long", typeof(long));
            keyValues.Add("int", typeof(int));
            keyValues.Add("integer", typeof(int));
            keyValues.Add("double", typeof(double));
            keyValues.Add("decimal", typeof(double));
            keyValues.Add("indexattribute", typeof(IndexAttribute));
        }

        public Type GetType(string value)
        {
            var retorno = typeof(string);

            retorno = keyValues.Where(x => x.Key == value).FirstOrDefault().Value;

            return retorno;
        }
    }
}
