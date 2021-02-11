using System;
using System.Collections.Generic;
using System.Text;

namespace VivoPagamentoJudiciais.Model.Entities
{
    public class FileUploadOCR
    {
        public int id { get; set; }
        public string idReturnOCR { get; set; }
        public string path { get; set; }
        public DateTime data { get; set; }
    }
}
