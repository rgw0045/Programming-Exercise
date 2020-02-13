using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NasaPhotos.Models
{
    public struct Cameras 
    {
        public string name;
        public string full_name;
    };

    public struct Rover 
    {
        public int id;
        public string name;
        public string landing_date;
        public string launch_date;
        public string status;
        public int max_sol;
        public string max_date;
        public int total_photos;
        public List<Cameras> cameras;
        
    };

    public class IndexViewModel 
    {
        public List<string> RoverImages { get; set; }

        public IndexViewModel() 
        {
            RoverImages = new List<string>();
        }
    }

    public class DataContainer
    {
        public List<ApiData> photos { get; set; }

        public DataContainer() 
        {
            photos = new List<ApiData>();
        }
    }

    public class ApiData
    {
        //properties
        public int id { get; set; }
        public int sol { get; set; }
        public string img_src { get; set; }
        public string earth_date { get; set; }
        public Rover rover { get; set; }

        public ApiData() 
        {
            id = 0;
            sol = 0;
            img_src = "";
            earth_date = "";
        }


    }
}
