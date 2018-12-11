using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Arena.Models
{
    public class ClubOwner_event
    {
        public int clubid { get; set; }
        public int noofteams { get; set; }
        public int noofteammembers { get; set; }
        public int availableplaces { get; set; }
        public float prize { get; set; }
        public float priceperteam { get; set; }
        public string name { get; set; }
        public DateTime event_start_time { get; set; }
        public DateTime event_end_time { get; set; }
    }
}