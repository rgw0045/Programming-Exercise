using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NasaPhotos.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;


namespace NasaPhotos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string APiKey = "cFRp2GG4xykl5KQN6ZmIzl0sNRomFRafrRLsuLTP";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            DataContainer PhotoContainer = new DataContainer();
            List<string> Dates = new List<string>();
            List<string> Queries = new List<string>();
            List<DataContainer> RoverData = new List<DataContainer>();
            IndexViewModel ViewModel = new IndexViewModel();
            Dates.AddRange(ReadFileContents());

            //format dates and create queries
            for (int i = 0; i < Dates.Count; i++)
            {
                string date = FormatDate(Dates[i]);

                if(date != "") 
                {
                    Queries.Add("https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?earth_date=" + date + "&api_key=" + APiKey);
                }
                
            }

            //run the queries

            using (HttpClient client = new HttpClient())
            {
                foreach (string query in Queries)
                {
                    var stringTask = client.GetStringAsync(query);

                    PhotoContainer = JsonConvert.DeserializeObject<DataContainer>(await stringTask);
                    //add images to image file

                    int counter = 0;
                    
                    //extract image from the photo container
                    foreach (ApiData i in PhotoContainer.photos) 
                    {                        
                        ViewModel.RoverImages.Add(i.img_src);
                        DownloadImage(i.img_src, i.id);
                        counter++;
                    }
                    DownloadImage(PhotoContainer.photos[0].img_src, PhotoContainer.photos[0].id);
                    RoverData.Add(PhotoContainer);
                }
            }

            //extract images from rover data
            

            return View(ViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public string WelcomeController() 
        {
            return "This is my Welcome Controller";
        }

        //[HttpPost]
        //public async Task<IActionResult> GetPhotos(string Date) 
        //{

        //    DataContainer PhotoContainer = new DataContainer();
        //    List<string> Dates = new List<string>();
        //    List<string> Queries = new List<string>();
        //    List<DataContainer> RoverData = new List<DataContainer>();
        //    IndexViewModel ViewModel = new IndexViewModel();
        //    Dates.AddRange(ReadFileContents());

        //    //format dates and create queries
        //    for (int i = 0; i < Dates.Count; i++)
        //    {
        //        string date = FormatDate(Dates[i]);

        //        if (date != "")
        //        {
        //            Queries.Add("https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?earth_date=" + date + "&api_key=" + APiKey);
        //        }

        //    }

        //    //run the queries

        //    using (HttpClient client = new HttpClient())
        //    {
        //        foreach (string query in Queries)
        //        {
        //            var stringTask = client.GetStringAsync(query);

        //            PhotoContainer = JsonConvert.DeserializeObject<DataContainer>(await stringTask);
        //            //add images to image file

        //            int counter = 0;

        //            //extract image from the photo container
        //            foreach (ApiData i in PhotoContainer.photos)
        //            {

        //                ViewModel.RoverImages.Add(i.img_src);
        //                counter++;
        //            }

        //            RoverData.Add(PhotoContainer);
        //        }
        //    }

        //    return View("~/Views/Home/PhotoGrid.cshtml", PhotoContainer);
        //}

        public bool DownloadImage(string url, int id) 
        {
            //create MARS folder on C drive
            string folderName = @"c:\MARS-PHOTOS";
            System.IO.Directory.CreateDirectory(folderName);

            string fileName = id.ToString() + ".JPG";

            try
            {
                WebClient client = new WebClient();
                client.DownloadFile(url, folderName + @"\" + fileName);

                return true;
            }
            catch (Exception e) 
            {
                return false;
            }
        }
        


        #region Helpers
        public string FormatDate(string unformattedDate)
        {
            string FormattedDate = "";

            try
            {
                int month = DateTime.Parse(unformattedDate).Month;
                int day = DateTime.Parse(unformattedDate).Day;
                int year = DateTime.Parse(unformattedDate).Year;

                FormattedDate = year.ToString() + "-" + month.ToString() + "-" + day.ToString();

                return FormattedDate;
            }
            catch (Exception e)
            {
                return FormattedDate;
            }
            
            
        }

        public List<string> ReadFileContents() 
        {
            string[] dateArray;
            List<string> dates = new List<string>();

            try
            {
                string fileContents = System.IO.File.ReadAllText("dates.txt");
                fileContents.Replace("\r\n", String.Empty);
                string newString = string.Join(" ", Regex.Split(fileContents, @"(?:\r\n|\n|\r)"));
                dateArray = Regex.Split(fileContents, @"(?:\r\n|\n|\r)");

                //convert into list
                

                foreach (string x in dateArray) 
                {
                    dates.Add(x);
                }

                return dates;
            }
            catch (Exception e) 
            {
                return dates;   
            }
            
        }
        #endregion
    }
}
