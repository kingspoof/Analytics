using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analytics
{
    public class CSVInfectionsAgeGroup
    {
        public void GenerateCSvInfectionsAgeGroup(string targetAgeGroup)
        {
            var Days = Extentions.GetSortedDatesList();
            var AgeGroups = Extentions.GetAgeGroups();
            var Input = GetFormattedData();
            var CSV = new StringBuilder();

            //print header
            CSV.Append("Date;");
            foreach (var age in AgeGroups)
                CSV.Append(age + "-Infections;" + age + "-Deaths;");
            CSV.AppendLine();

            foreach (var day in Days)
            {
                CSV.Append(day + ";");

                foreach (var age in AgeGroups)
                {
                    CSV.Append(
                        Input[age][day].Sum(c => Convert.ToInt32(c.NumberOfInfections))
                        + ";" +
                        Input[age][day].Sum(c => Convert.ToInt32(c.NumberOfDeaths))
                        + ";"
                    );
                    
                    foreach(var i in Input[age][day])
                        SwitzerlandStats.ByAgeGroup[age].Add(i);

                }

                CSV.AppendLine();
            }

            File.WriteAllText(Program.Output_AgeGroups,CSV.ToString());
        }
        private Dictionary<string, Dictionary<string, List<Case>>> GetFormattedData()
        {
            var input = Program.rows;
            var output = new Dictionary<string, Dictionary<string, List<Case>>>();

            //Make the following dictionary: AgeGroup -> Dates -> Cases
            foreach (var AgeGroup in input.GroupBy(c => c.AgeClass))
            {
                var AgeValue = new Dictionary<string, List<Case>>();

                foreach (var date in AgeGroup.ToList().GroupBy(d => d.Date))
                    AgeValue.Add(date.Key, date.ToList());

                output.Add(AgeGroup.Key, AgeValue);
            }

            return output;
        }
    }
}