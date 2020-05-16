using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GridAndCloud.DataImport
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = File.ReadAllText(@"");
            List<Monitor> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Monitor>>(text).Where(x => x.Color == null).ToList();

            string server = "";
            string database = "";
            string uid = "";
            string password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            var sb = new StringBuilder();

            sb.Append("INSERT INTO Monitor(ItemName, Price, Provider, Photos, PhotosLinks, Characteristics, Color) VALUES ");

            foreach (var monitor in obj)
            {
                sb.Append("('" + monitor.ItemName.Trim().Replace("'", "") + 
                    "', '" + monitor.Price.Trim().Replace("'", "") + 
                    "', '" + monitor.Provider.Trim().Replace("'", "") + 
                    "', '" + monitor.Photos.Trim().Replace("'", "") + 
                    "', '" + monitor.PhotosLinks.Trim().Replace("'", "") + 
                    "', '" + monitor.Characteristics.Trim().Replace("'", "") + 
                    "', '" + monitor.Color + "'),");
            }

            File.WriteAllText(@"", sb.ToString());
        }
    }
}
