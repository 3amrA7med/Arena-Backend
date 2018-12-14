using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Arena.Models
{
    public class ClubOwner_maintanance
    {
        public int clubid { get; set; }
        public int pitch_no { get; set; }
        public float cost { get; set; }
        public string description { get; set; }
        public DateTime maintanance_start_date { get; set; }
        public DateTime maintanance_end_date { get; set; }

    }
}