using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analytics
{
    public class CSVFormatter
    {
        ///
        /// Creates a CSV with Infections per Day
        /// 
        public void CreateCSVWithInfectionsPerDay()
        {
            Console.WriteLine("Creating CSV with Infections and deaths per Day in Switzerland.");

            var temp = Program.rows.GroupBy(e => e.Date);
            var allrows = Program.rows.Where(e => e.Date != "NA").ToList();
            var csv = new StringBuilder();
            csv.AppendLine("Date;Infections;Deaths");
            foreach (var t in temp)
            {
                if (t.Key != "NA")
                {
                    S_Day d = new S_Day()
                    {
                        Date = t.Key,
                        Infections = t.Sum(e => Convert.ToInt32(e.NumberOfInfections)),
                        Deaths = t.Sum(e => Convert.ToInt32(e.NumberOfDeaths)),
                    };

                    //Write if not older than 7 days
                    if(Convert.ToDateTime(d.Date) > DateTime.Now.AddDays( - 100 ))
                    {
                        Console.WriteLine(d.GetCSVLine().Replace(";","     "));
                        Program.last7Days.Add(d);
                    }
                    
                    csv.AppendLine(d.GetCSVLine());
                }
            }
            File.WriteAllText(Program.Output_PathInfectionPerDay, csv.ToString());
            
            temp = null;
            allrows = null;
            csv = null;
        }
    }
}