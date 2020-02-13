using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace NasaPhotos.Models
{
    public class InterfaceModel
    {
        private static readonly HttpClient client = new HttpClient();
        private string Date { get; set; }
        private string PhotoData { get; set; }

        private static string APiKey = "cFRp2GG4xykl5KQN6ZmIzl0sNRomFRafrRLsuLTP";


        private static async Task ProcessPhotos()
        {

            var stringTask = client.GetStringAsync("https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?earth_date=2015-6-3&api_key=" + APiKey);

            var msg = await stringTask;
            Console.Write(msg);
        }
    }
}
