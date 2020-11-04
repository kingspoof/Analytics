using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analytics
{
    public class CSVAllCantonsOneSheet
    {
        public void PrintAllCantonsOneSheet()
        {
            var data = GetAllDataAndFormatThem();
            var csv = new StringBuilder();
            var dates = new List<string>();

            

            csv.Append("Date;");
            //generate header
            foreach (var e in data)
                csv.Append(e.Key + "-Infections;" + e.Key + "-Deaths;");

            // foreach (var e in data.Values.First())
            //     if (e.Key != "NA" && !String.IsNullOrEmpty(e.Key))
            //         dates.Add(e.Key);

            //dates.OrderBy(e => Convert.ToDateTime(Convert.ToDateTime(e).ToString("dd.MM.yyyy")));

            csv.AppendLine();
            foreach (string date in dates)
            {
                csv.Append(date + ";");
                foreach (var e in data)
                {
                    csv.Append(
                        e.Value[date].Sum(e => Convert.ToInt32(e.NumberOfInfections))
                        + ";" +
                        e.Value[date].Sum(e => Convert.ToInt32(e.NumberOfDeaths))
                        + ";");
                }

                csv.AppendLine();
            }
            File.WriteAllText(Program.Output_AllCantons, csv.ToString());
            Console.WriteLine("That has taken me to long");
        }
        private Dictionary<string, Dictionary<string, List<Case>>> GetAllDataAndFormatThem()
        {
            var data = new Dictionary<string, Dictionary<string, List<Case>>>();
            foreach (var e in Program.rows.GroupBy(e => e.Canton))
            {
                string ca = e.Key;
                var cases = e.ToList().GroupBy(e => e.Date);
                var temp = new Dictionary<string, List<Case>>();
                foreach (var n in cases)
                {
                    temp.Add(n.Key, n.ToList());
                }

                data.Add(e.Key, temp);
            }

            return data;
        }
    }
}