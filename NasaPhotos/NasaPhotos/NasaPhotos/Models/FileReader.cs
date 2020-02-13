using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NasaPhotos.Models
{
    public class FileReader
    {
        public List<string> PhotoDates { get; set; }



        public string FormatDate(string unformattedDate)
        {
            string FormattedDate = "";


            int month = DateTime.Parse(unformattedDate).Month;
            int day = DateTime.Parse(unformattedDate).Day;
            int year = DateTime.Parse(unformattedDate).Year;

            //create formatted string
            FormattedDate = year.ToString() + "-" + month.ToString() + "-" + day.ToString();

            return FormattedDate;
        }



    }
}
