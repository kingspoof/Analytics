using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Analytics
{

    public class S_Day
    {
        public string Date { get; set; }
        public double Infections { get; set; }
        public double Deaths { get; set; }

        public string GetCSVLine()
        {
            return $"{this.Date};{this.Infections};{this.Deaths}";
        }
    }

}