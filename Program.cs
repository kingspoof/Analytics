using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analytics
{
    public class Program
    {
        public static InformationLoader InformationLoader = new InformationLoader();
        public static ExcelDownloader ExcelDownloader = new ExcelDownloader();
        public static CSVFormatterCanton CSVFormatterCanton = new CSVFormatterCanton();
        public static CSVFormatter CSVFormatter = new CSVFormatter();
        public static CSVInfectionsAgeGroup CSVInfectionsAgeGroup = new CSVInfectionsAgeGroup();
        public static CSVAllCantonsOneSheet CSVAllCantonsOneSheet = new CSVAllCantonsOneSheet();
        public static ConsoleGraph ConsoleGraph = new ConsoleGraph();


        public static string InputPath, InputFilePath, Input_WebPathToEcxel,
                            Output_Directory, Output_PathInfectionPerDay, Output_ContonDirectory,
                            Output_TargetAgeGroup, Output_AllCantons, Output_AgeGroups = "";

        public static List<Case> rows = new List<Case>();
        public static List<S_Day> last7Days = new List<S_Day>();
        static void Main(string[] args)
        {

            bool rundefinitely = true;
            if (rundefinitely)
            {
                var start = DateTime.Now;

                //Create Output Directories
                CreatePathsNDirectories();

                //Download Excel-File
                ExcelDownloader.DownloadExcel();

                //Transform Excel to CSV File
                ExcelDownloader.TransFormExcelToCSV();

                //Load all Rowns from Input CSV
                rows = InformationLoader.loadcases();

                //Generate CSV with Infections / Day
                CSVFormatter.CreateCSVWithInfectionsPerDay();

                //Generate csv for age groups
                CSVInfectionsAgeGroup.GenerateCSvInfectionsAgeGroup(AgeGroups._Ü80);

                //Generate all Cantons in one sheet
                CSVAllCantonsOneSheet.PrintAllCantonsOneSheet();

                Console.WriteLine($"We are fing efficient: 370'000 Data Rows and 3 files in {(DateTime.Now - start).TotalSeconds} seconds.");

                //generate graph for user convinience
                ConsoleGraph.GenerateGraph();

            }
            else
            {
                while (true)
                {
                    //Load Phonenumbers

                    //GetData

                    //SendData
                }
            }
        }

        private static void CreatePathsNDirectories()
        {
            Console.WriteLine("Create Paths and Directories");

            InputPath = Environment.CurrentDirectory + @"\Inputdata.CSV";
            InputFilePath = Environment.CurrentDirectory + @"\InputExcel.xlsx";
            Output_Directory = Environment.CurrentDirectory + $@"\Output-{DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss")}";
            Output_TargetAgeGroup = Output_Directory + @"\InfectionsPerAgeGroup.CSV";
            Output_PathInfectionPerDay = Output_Directory + @"\InfectionsPerDay.CSV";
            Output_ContonDirectory = Output_Directory + @"\Cantons";
            Output_AllCantons = Output_Directory + @"\Cantons.csv";
            Output_AgeGroups = Output_Directory + @"\AgeGroups.csv";

            Input_WebPathToEcxel = "https://www.bag.admin.ch/dam/bag/de/dokumente/mt/k-und-i/aktuelle-ausbrueche-pandemien/2019-nCoV/covid-19-basisdaten-fallzahlen.xlsx.download.xlsx/Dashboards_1&2_COVID19_swiss_data_pv.xlsx";

            Directory.CreateDirectory(Output_Directory);
            //Directory.CreateDirectory(Output_ContonDirectory);
        }
    }
}
