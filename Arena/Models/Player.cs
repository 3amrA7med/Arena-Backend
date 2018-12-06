using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Arena.Models
{
    public class Player
    {
        public string username { get; set; }
        public string password { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string email { get; set; }
        public double phone { get; set; }
        public double visa { get; set; }
        public DateTime bdate { get; set; }
        public string academyName { get; set; }
        public int clubId { get; set; }
    }
}