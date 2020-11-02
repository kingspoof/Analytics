using System.Collections.Generic;


namespace Analytics
{
    public class DataStorrage
    {
        public Dictionary<string, Dictionary<string, Dictionary<string, Case>>> Data = new Dictionary<string, Dictionary<string, Dictionary<string, Case>>>();

        public DataStorrage()
        {
            //Day, 
            var localdata = Program.rows;
        }
    }
}