using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VivoPagamentoJudiciais.Models
{
    public class FileUploadOCR
    {
        public int id { get; set; }
        public string idReturnOCR { get; set; }
        public string path { get; set; }
        public DateTime data { get; set; }
    }
}
