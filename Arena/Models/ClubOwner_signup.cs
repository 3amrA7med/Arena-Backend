using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Arena.Models
{
    public class ClubOwner_signup
    {
        public string username { get; set; }
        public string password { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string email { get; set; }
        public double phone { get; set; }
        public string officeHours { get; set; }
        //  public DateTime sdate { get; set; }
        public string clubName { get; set; }
        public string clubCity { get; set; }
        public string clubStreet { get; set; }
    }
}