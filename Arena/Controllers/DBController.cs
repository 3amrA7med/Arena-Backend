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

        [Route("api/DB/PostOwnerViewAcadamy")]
        [HttpPost]
        public DataTable PostOwnerViewAcadamy([FromBody]ClubOwner_clubid c)
        {

            System.Diagnostics.Debug.WriteLine("Inside event_Post********************************** ");
            DataTable result = handler.clubOwner_viewacademy(c);
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

        [Route("api/DB/PostOwnerPitchNumber")]
        [HttpPost]
        public DataTable PostOwnerPitchNumber([FromBody]ClubOwner_clubid c)
        {

            System.Diagnostics.Debug.WriteLine("Inside event_Post********************************** ");
            DataTable result = handler.clubOwner_pitchno(c);
            return result;
        }

        [Route("api/DB/PostOwnerClubId")]
        [HttpPost]
        public DataTable PostOwnerClubId ([FromBody]ClubOwner_username u)
        {
            System.Diagnostics.Debug.WriteLine("Inside getid_Post********************************** ");
            DataTable result = handler.clubOwner_getid(u);
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
