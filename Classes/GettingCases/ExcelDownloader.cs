using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Data;
using OfficeOpenXml;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

namespace Analytics
{

    public class ExcelDownloader
    {
        public void DownloadExcel()
        {
            Console.WriteLine("Downloading Excel from BAG.CH");
            var client = new WebClient();
            client.DownloadFile(Program.Input_WebPathToEcxel, Program.InputFilePath);
        }

        public void TransFormExcelToCSV()
        {
            Console.WriteLine("Transforming excel to csv file.");
            using (var excelPack = new ExcelPackage())
            {
                var csv = new StringBuilder();

                //load excel stream
                using (var stream = File.OpenRead(Program.InputFilePath))
                {
                    excelPack.Load(stream);
                }

                var worksheet = excelPack.Workbook.Worksheets[0];

                DataTable exceltable = new DataTable();
                foreach (var firstRowcell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
                {
                    if (!string.IsNullOrEmpty(firstRowcell.Text))
                    {
                        string firstColumn = string.Format("Column {0}", firstRowcell.Start.Column);
                        exceltable.Columns.Add(firstRowcell.Text);
                    }
                }

                for (int rownNumber = 2; rownNumber <= worksheet.Dimension.End.Row; rownNumber++)
                {
                    var wsRow = worksheet.Cells[rownNumber, 1, rownNumber, exceltable.Columns.Count];

                    string toAdd = "";

                    foreach (var cell in wsRow)
                        toAdd += cell.Text + ";";

                    csv.AppendLine(toAdd.Substring(0, toAdd.Length - 1));
                }
                File.WriteAllText(Program.InputPath, csv.ToString());
            }
        }
    }

}