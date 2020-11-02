using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analytics
{
    public class InformationLoader
    {
        public List<Case> loadcases()
        {
            Console.WriteLine("Loading and formatting cases");
            List<Case> rows = new List<Case>();

            int failed = 0;
            using (StreamReader sr = new StreamReader(Program.InputPath))
            {
                string currentline;

                while ((currentline = sr.ReadLine()) != null)
                {
                    string[] splitted = currentline.Split(';');

                    if (splitted.Length == 10)
                    {
                        try
                        {
                            rows.Add(new Case()
                            {
                                Date = splitted[1].Equals("NA") ? splitted[8] : splitted[1],
                                Canton = splitted[2],
                                AgeClass = splitted[3],
                                Gender = (splitted[4]),
                                NumberOfInfections = (splitted[7]),
                                NumberOfDeaths = (splitted[9]),

                                //CreationDate = (splitted[0]),
                                //Geschlecht = splitted[5],
                                //Sexe = splitted[6],
                            });
                        }
                        catch
                        {
                            failed++;
                        }
                    }
                }
            }
            Console.WriteLine("Succeded: " + rows.Count + "     Failed: " + failed);

            return rows;
        }
    }
}