using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Arena.Models;
using System.Data;

namespace Arena.Controllers
{
    public class DBController : ApiController
    {
        DBHandler handler;

        public DBController()
        {
            handler = new DBHandler();
        }
        //----------------------------------------GET FUNCTIONS---------------------------------

/* 
 * notes to my friends (JIMJIM,SHEDO,DODA)
 * // Type-specific constraints
{ "bool", typeof(BoolRouteConstraint)
    },
{ "datetime", typeof(DateTimeRouteConstraint)
},
{ "decimal", typeof(DecimalRouteConstraint) },
{ "double", typeof(DoubleRouteConstraint) },
{ "float", typeof(FloatRouteConstraint) },
{ "guid", typeof(GuidRouteConstraint) },
{ "int", typeof(IntRouteConstraint) },
{ "long", typeof(LongRouteConstraint) },
string is the defult value   
    */



        // GET: api/DB
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/DB/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("api/DB/Book/{date:datetime}/{cid:int}/{pid:int}")]
        [HttpGet]
        public DataTable GetBooked( DateTime date,int cid,int pid)
        {
            System.Diagnostics.Debug.WriteLine("Inside bool********************************** ");
            DataTable result = handler.availableSchedule(date,cid,pid);
            return result;      
        }
        [Route("api/DB/Book/getCity")]
        [HttpGet]
        public DataTable getCity()
        {
            System.Diagnostics.Debug.WriteLine("Inside bool********************************** ");
            DataTable result = handler.getCitiesBooking();
            return result;
        }
        //"api/DB/Book/insertBooking/b/3/1/2018-12-12/12:00/100/100/"
        [Route("api/DB/Book/insertBooking/{user}/{selclub:int}/{selpitchno:int}/{date}/{date2}/{paid:int}/{unpaid:int}")]
        [HttpGet]
        public DataTable insertBooking(string user,int  selclub,int selpitchno,string date,string date2, int paid,int unpaid)
        {
            System.Diagnostics.Debug.WriteLine("Inside bool********************************** ");
            DataTable result = handler.insertBookingDB(user, selclub, selpitchno, date + " " + date2 + ":00", paid, unpaid);
            return result;
        }
        [Route("api/DB/Book/getClubs/{city}")]
        [HttpGet]
        public DataTable getClubs(string city)
        {
            System.Diagnostics.Debug.WriteLine("Inside bool********************************** ");
            DataTable result = handler.getClubsDB(city);
            return result;
        }
        [Route("api/DB/Book/getPitches/{id:int}")]
        [HttpGet]
        public DataTable getPitches(int id)
        {
            System.Diagnostics.Debug.WriteLine("Inside bool********************************** ");
            DataTable result = handler.getPitchesDB(id);
            return result;
        }
        [Route("api/DB/GetEvents/{username}/{date:datetime}")]
        [HttpGet]
        public DataTable GetEvents(string username,DateTime date)
        {
            System.Diagnostics.Debug.WriteLine("Inside GETEVENTS********************************** ");
            DataTable result =handler.GetEvents(date,username);
            return result;
        }

        [Route("api/DB/GetReservations/{username}/{date:datetime}")]
        [HttpGet]
        public DataTable GetReservations(string username,DateTime date)
        {
            System.Diagnostics.Debug.WriteLine("Inside GETReser********************************** ");
            DataTable result = handler.GetReservations(date, username);
            return result;
        }

        [Route("api/DB/GetMaint/{username}/{date:datetime}")]
        [HttpGet]
        public DataTable GetMaint(string username, DateTime date)
        {
            System.Diagnostics.Debug.WriteLine("Inside GETmaint********************************** ");
            DataTable result = handler.GetMaint(date, username);
            return result;
        }
        //--------------------------------------------------------------------------
        //---------------------------------------POST FUNCTIONS--------------------
        // POST: api/DB
        public DataTable Post([FromBody]Signin s)
        {
            System.Diagnostics.Debug.WriteLine("Inside Account_Post********************************** ");
            DataTable result = handler.check_signin(s);
            return result;
        }
        //p.username, p.password, p.fname, p.lname,
        // p.email,Convert.ToDouble(p.phone), Convert.ToDouble(p.visa), p.bdate
        [Route("api/DB/PostPlayer")]
        [HttpPost]
        public DataTable PostPlayer([FromBody]Player p)
        {
            System.Diagnostics.Debug.WriteLine("Inside Player_Post********************************** ");
            DataTable result = handler.player_signup(p);
            return result;
        }

        [Route("api/DB/PostOwner")]
        [HttpPost]
        public DataTable PostOwner([FromBody]ClubOwner_signup c)
        {
            System.Diagnostics.Debug.WriteLine("Inside owner_Post********************************** ");
            DataTable result = handler.clubOwner_signup(c);
            return result;
        }
        //--------------------------------------------------------------------------
        //---------------------------------------PUT FUNCTIONS----------------------
        // PUT: api/DB/5
        public void Put(int id, [FromBody]string value)
        {
        }
        //--------------------------------------------------------------------------
        //---------------------------------------DELETE FUNCTIONS-------------------
        // DELETE: api/DB/5
        public void Delete(int id)
        {
        }


    }
}
