using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Arena.Models
{
    public class ClubOwner_pitch
    {
        public int clubid { get; set; }
        public int pitch_no { get; set; }
        public float price { get; set; }
        public DateTime creation_date { get; set; }
        public int capacity { get; set; }
        public int type { get; set; }
    }
}