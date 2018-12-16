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
        

        [Route("api/DB/Book/getClubs/{city}")]
        [HttpGet]
        public DataTable getClubs(string city)
        {
            System.Diagnostics.Debug.WriteLine("Inside bool********************************** ");
            DataTable result = handler.getClubsDB(city);
            return result;
        }
        [Route("api/DB/Book/getAcadAllClubs")]
        [HttpGet]
        public DataTable getAcadAllClubs()
        {
            System.Diagnostics.Debug.WriteLine("Inside bool********************************** ");
            DataTable result = handler.getAcadAllClubsDB();
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

        [Route("api/DB/GetMyAcademy/{username}")]
        [HttpGet]
        public DataTable GetMyAcademy(string username)
        {
            System.Diagnostics.Debug.WriteLine("Inside GET********************************** ");
            DataTable result = handler.GetMyAcademy(username);
            return result;  
        }

        [Route("api/DB/GetPastEvents/{username}")]
        [HttpGet]
        public DataTable GetPastEvents(string username)
        {
            System.Diagnostics.Debug.WriteLine("Inside GET********************************** ");
            DataTable result = handler.GetPastEvents(username);
            return result;
        }

        [Route("api/DB/GetOwnerClubId/{username}")]
        [HttpGet]
        public DataTable GetOwnerClubId(string username)
        {
            System.Diagnostics.Debug.WriteLine("Inside getid_Post********************************** ");
            DataTable result = handler.clubOwner_getid(username);
            return result;
        }

        [Route("api/DB/GetOwnerPitchNumber/{clubid}")]
        [HttpGet]
        public DataTable GetOwnerPitchNumber(int clubid)
        {

            System.Diagnostics.Debug.WriteLine("Inside event_Post********************************** ");
            DataTable result = handler.clubOwner_pitchno(clubid);
            return result;
        }

        [Route("api/DB/GetOwnerViewAcadamy/{clubid}")]
        [HttpGet]
        public DataTable GetOwnerViewAcadamy(int clubid)
        {

            System.Diagnostics.Debug.WriteLine("Inside event_Post********************************** ");
            DataTable result = handler.clubOwner_viewacademy(clubid);
            return result;
        }
        [Route("api/DB/GetPastReservations/{username}")]
        [HttpGet]
        public DataTable GetPastReservations(string username)
        {
            System.Diagnostics.Debug.WriteLine("Inside GET********************************** ");
            DataTable result = handler.GetPastReservations(username);
            return result;
        }

        [Route("api/DB/GetUpcomingEvents/{username}")]
        [HttpGet]
        public DataTable GetUpcomingEvents(string username)
        {
            System.Diagnostics.Debug.WriteLine("Inside GET********************************** ");
            DataTable result = handler.GetUpcomingEvents(username);
            return result;
        }

        [Route("api/DB/GetUpcomingReservations/{username}")]
        [HttpGet]
        public DataTable GetUpcomingReservations(string username)
        {
            System.Diagnostics.Debug.WriteLine("Inside GET********************************** ");
            DataTable result = handler.GetUpcomingReservations(username);
            return result;
        }
       
        [Route("api/DB/GetAvailableEvents/{username}/{city}")]
        [HttpGet]
        public DataTable GetAvailableEvents(string username,string city)
        {
            System.Diagnostics.Debug.WriteLine("Inside GET********************************** ");
            DataTable result = handler.GetAvailableEvents(username, city);
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
        [Route("api/DB/Enroll")]
        [HttpPost]
        public DataTable Enroll([FromBody]Player_Enrollment p)
        {
            System.Diagnostics.Debug.WriteLine("Inside Player_Post********************************** ");
            DataTable result = handler.enrollEvent(p);
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

        [Route("api/DB/PostOwnerMaintanance")]
        [HttpPost]
        public DataTable PostOwnerMaintanance([FromBody]ClubOwner_maintanance m)
        {
            
            System.Diagnostics.Debug.WriteLine("Inside maintanance_Post********************************** ");
            DataTable result = handler.clubOwner_maintanance(m);
            return result;
        }

        [Route("api/DB/PostOwnerPitch")]
        [HttpPost]
        public DataTable PostOwnerPitch([FromBody]ClubOwner_pitch p)
        {

            System.Diagnostics.Debug.WriteLine("Inside pitch_Post********************************** ");
            DataTable result = handler.clubOwner_pitch(p);
            return result;
        }

        [Route("api/DB/PostOwnerEvent")]
        [HttpPost]
        public DataTable PostOwnerEvent([FromBody]ClubOwner_event e)
        {

            System.Diagnostics.Debug.WriteLine("Inside event_Post********************************** ");
            DataTable result = handler.clubOwner_event(e);
            return result;
        }

        [Route("api/DB/PostOwnerUpdateAcademy")]
        [HttpPost]
        public DataTable PostOwnerUpdateAcademy([FromBody]ClubOwner_academy a)
        {

            System.Diagnostics.Debug.WriteLine("Inside event_Post********************************** ");
            DataTable result = handler.clubOwner_academy(a);
            return result;
        }



        [Route("api/DB/PostOwnerAddAcadamy")]
        [HttpPost]
        public DataTable PostOwnerAddAcadamy([FromBody]ClubOwner_addacademy c)
        {

            System.Diagnostics.Debug.WriteLine("Inside event_Post********************************** ");
            DataTable result = handler.clubOwner_addacademy(c);
            return result;
        }



        [Route("api/DB/Book/insertBooking")]
        [HttpPost]
        public DataTable insertBooking([FromBody]PlayerBooking p)
        {
            System.Diagnostics.Debug.WriteLine("Inside bool********************************** ");
            DataTable result = handler.insertBookingDB(p);
            return result;
        }


        //--------------------------------------------------------------------------
        //---------------------------------------PUT FUNCTIONS----------------------
        // PUT: api/DB/5
        [Route("api/DB/UpdatePlayer")]
        [HttpPost]
        public DataTable UpdatePlayer([FromBody]Player p)
        {
            System.Diagnostics.Debug.WriteLine("Inside update_player********************************** ");
            DataTable result = handler.UpdatePlayer(p);
            return result;
        }

        [Route("api/DB/UnsubscribeAcademy")]
        [HttpPost]
        public DataTable Unsubscribe([FromBody]Player p)
        {
            System.Diagnostics.Debug.WriteLine("Inside update_player********************************** ");
            DataTable result = handler.Unsubscribe(p);
            return result;
        }
        [Route("api/DB/SubscribeAcademy")]
        [HttpPost]
        public DataTable Subscribe([FromBody]Player_Subscription p)
        {
            System.Diagnostics.Debug.WriteLine("Inside update_player********************************** ");
            DataTable result = handler.SubscribeDB(p);
            return result;
        }
        //--------------------------------------------------------------------------
        //---------------------------------------DELETE FUNCTIONS-------------------
        // DELETE: api/DB/5
        public void Delete(int id)
        {
        }

        [Route("api/DB/DeleteEvent/{id:int}/{username}")]
        [HttpDelete]
        public int DeleteEvent(int id, string username)
        {
            return handler.DeleteEvent(id, username);
        }

        [Route("api/DB/DeleteReservation/{num:int}/{username}/{date:datetime}/{time}")]
        [HttpDelete]
        public int DeleteReservation(int num, string username, DateTime date, String time)
        {
           return handler.DeleteReservation(date,num, username, time);
        }

        [Route("api/DB/DeleteMaint/{num:int}/{username}/{date:datetime}/{time}/{minutes}")]
        [HttpDelete]
        public int DeleteMaint(int num, string username, DateTime date, String time,String minutes)
        {
           return handler.DeleteMaint(date, num, username, time,minutes);
        }

    }
}
