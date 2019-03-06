using System;
using System.Collections.Generic;
using System.Text;

namespace SamsonTechMobileApp
{
    public class Date
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public Date(string date)
        {
            string[] dataSplit = date.Split('/');

            Day = int.Parse(dataSplit[0]);
            Month = int.Parse(dataSplit[1]);
            Year = int.Parse(dataSplit[2]);
        }
    }
}
