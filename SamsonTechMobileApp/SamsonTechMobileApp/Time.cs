using System;
using System.Collections.Generic;
using System.Text;

namespace SamsonTechMobileApp
{
    public class Time
    {
        public int Seconds { get; set; }
        public int Minutes { get; set; }
        public int Hours { get; set; }

        public Time(string time)
        {
            string[] timeSplit = time.Split(':');

            Hours = int.Parse(timeSplit[0]);
            Minutes = int.Parse(timeSplit[1]);
            Seconds = int.Parse(timeSplit[2]);
        }
    }
}
