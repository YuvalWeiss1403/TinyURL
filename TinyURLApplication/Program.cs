using System;
using System.Collections;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;



namespace TinyURLApplication
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            var connectionString = "mongodb://localhost:27017";

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("URL");
            

            string text = System.IO.File.ReadAllText(@"C:\Users\Yuval\source\repos\TinyURLApp\TinyURLApplication\cache.json");
            if(text==null)
            {
                var document = BsonSerializer.Deserialize<BsonDocument>("empty");
                var collection = database.GetCollection<BsonDocument>("URL");
                collection.InsertOneAsync(document);
            }
            else
            {
                var document = BsonSerializer.Deserialize<BsonDocument>(text);
                var collection = database.GetCollection<BsonDocument>("URL");
                collection.InsertOneAsync(document);
            }


            
        }
    }
}

