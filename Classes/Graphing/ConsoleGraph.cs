using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analytics
{
    public class ConsoleGraph
    {
        public void GenerateGraph()
        {
            var data = Program.last7Days;
            var columns = new List<Column>();
            var biggest = data.Max(e => e.Infections);
            var numberOffSteps = 20;
            var step = Math.Round(biggest / numberOffSteps);

            foreach(var d in data)
            {
                columns.Add(
                    new Column()
                    {
                        date = Convert.ToDateTime(d.Date),
                        rawData = (int)d.Infections,
                        height = (int)Math.Round(d.Infections / step),
                    }
                );
            }

            columns.OrderBy(e => e.date);
            var result = new List<string>();
            for(int i = 0; i < numberOffSteps; i++)
            {
                string temp = "";
                foreach(var e in columns)
                {
                    if(e.height <= i)
                    {
                        temp += " ";
                    }
                    else
                    {
                        temp += "-";
                    }
                }
                result.Add(temp);
            }


            for(int i = result.Count() - 1 ; i >= 0; i--){
                Console.WriteLine(result[i]);
            }
            data = null;
        }
    }
}