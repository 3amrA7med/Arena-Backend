using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Arena.Models
{
    public class PlayerBooking
    {
        public string user { get; set; }
        public int selpitchno { get; set; }
        public int paid { get; set; }
        public int unpaid { get; set; }
        public int selclub { get; set; }
        public string date { get; set; }
        public string date2 { get; set; }


    }
}