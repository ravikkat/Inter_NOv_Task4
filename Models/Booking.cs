using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssesmentTask2.Models
{
    public class Booking
    {
        public long Id { get; set; }
        public string UpdatedTime { get; set; }


        public string ChartName { get; set; }

        public string USD_RATE { get; set; }
        public string USD_RATE_FLOAT { get; set; }
    }

    public class Child
    {
        public Time1 time { get; set; }
        public int age { get; set; }
        public string chartname { get; set; }
        public bpi bpi { get; set; }
    }
    public class Time1
    {
        public string updated { get; set; }
        public string updatedISO { get; set; }

    }

    public class bpi
    {
        public USD usd { get; set; }

    }

    public class USD
    {
        public string code { get; set; }
        public string rate { get; set; }
        public string rate_float { get; set; }

    }
}
