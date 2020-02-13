using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace NasaPhotos
{
    public class Program
    {
        private static HttpClient client { get; set; }
        private static string APiKey = "cFRp2GG4xykl5KQN6ZmIzl0sNRomFRafrRLsuLTP";

        public static void Main(string[] args)
        {
            //await ProcessPhotos();
            CreateHostBuilder(args).Build().Run();
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        //private static async Task ProcessPhotos()
        //{
        //    client = new HttpClient();

        //    var stringTask = client.GetStringAsync("https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?earth_date=2015-6-3&api_key=" + APiKey);

        //    var msg = await stringTask;
        //    Console.Write(msg);
        //}
    }
}
