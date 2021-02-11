using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VivoPagamentoJudiciais.Utilities.File
{
    public class FileParse
    {
        public FileParse()
        {

        }
        public static string ToBase64String(string filePath)
        {
            byte[] buffer = System.Text.Encoding.Unicode.GetBytes(filePath);
            return System.Convert.ToBase64String(buffer);
        }
    }
}
