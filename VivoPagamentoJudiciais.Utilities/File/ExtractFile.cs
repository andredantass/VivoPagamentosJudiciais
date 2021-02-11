using Ionic.Zip;
using System.IO;
using System.Linq;

namespace VivoPagamentoJudiciais.Utilities.File
{
    public class ExtractFile
    {
        public static MemoryStream Extract(string filePath)
        {
            var outputStream = new MemoryStream();

            using (ZipFile zip = ZipFile.Read(filePath))
            {
                zip.FirstOrDefault().Extract(outputStream);
            }

            return outputStream;
        }
    }
}
