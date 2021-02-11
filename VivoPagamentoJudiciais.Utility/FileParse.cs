using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;

namespace VivoPagamentoJudiciais.Utility
{
    public static class FileParse
    {
        public static string ToBase64String(string filePath)
        {
            byte[] buffer = System.Text.Encoding.Unicode.GetBytes(filePath);
            return System.Convert.ToBase64String(buffer);
        }
        public static List<string> ExtractValuesExcelSheet(string excelSheetName)
        {
            int startColumn = 2;
            int startRow = 1;
            List<string> lstExtractedData = new List<string>();

            try
            {
                var package = new ExcelPackage(new System.IO.FileInfo(excelSheetName));
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];

                object data = null;

                while (data == null)
                {
                    data = workSheet.Cells[startColumn, startRow].Value;
                    if (data != null && data.ToString().Trim() != "")
                    {
                        lstExtractedData.Add(data.ToString());
                    }
                }

                return lstExtractedData;
            }
            catch(Exception ex)
            {
                return null;
            }
            return null;

        }
    }
}

