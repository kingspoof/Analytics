using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analytics
{
    public static class Extentions
    {
        public static List<string> GetSortedDatesList()
        {
            List<string> output = new List<string>();

            var test = Program.rows.OrderBy(e => e.Date).GroupBy(e => e.Date).ToList();

            foreach (var d in test)
                if (!String.IsNullOrEmpty(d.Key) && d.Key != "NA")
                    output.Add(d.Key);

            return output;
        }
        public static List<string> GetAgeGroups()
        {
            return new List<string>()
            {
                AgeGroups._0bis10,
                AgeGroups._10bis20,
                AgeGroups._20bis30,
                AgeGroups._30bis40,
                AgeGroups._40bis50,
                AgeGroups._50bis60,
                AgeGroups._60bis70,
                AgeGroups._70bis80,
                AgeGroups._Ü80
            };
        }
    }
}