using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Analytics
{
    public class CSVFormatterCanton
    {
        public void CenerateCSVForCantons()
        {
            Console.WriteLine("Generate CSV for Contons in switzerland");

            var grouped = Program.rows.GroupBy(e => e.Canton);

            foreach(var group in grouped)
            {
                var csv = new StringBuilder();
                csv.AppendLine(group.Key);
                csv.AppendLine("Date;Infections;Deaths");
                foreach(var g in group.GroupBy(e => e.Date))
                {
                    S_Day d = new S_Day(){
                        Date = g.Key,
                        Infections = Convert.ToInt32(g.Sum(e => Convert.ToInt32(e.NumberOfInfections))),
                        Deaths = Convert.ToInt32(g.Sum(e => Convert.ToInt32(e.NumberOfDeaths))),
                    };
                    csv.AppendLine(d.GetCSVLine());
                    d = null;
                }

                File.WriteAllText(Program.Output_ContonDirectory + $@"\{group.Key}.csv", csv.ToString());
                csv = null;
            }

            grouped = null;
        }
    }
}