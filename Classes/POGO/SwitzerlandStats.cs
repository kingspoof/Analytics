using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analytics
{
    public static class SwitzerlandStats
    {
        public static Dictionary<string, List<Case>> ByAgeGroup = new Dictionary<string, List<Case>>();
        static SwitzerlandStats()
        {
            foreach (string AgeGroup in Extentions.GetAgeGroups())
            {
                ByAgeGroup.Add(AgeGroup, new List<Case>());
            }
        }
    }
}