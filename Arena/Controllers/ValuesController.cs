using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Arena.Controllers
{
   
    public class ValuesController : ApiController
    {
        // GET api/values
        ValuesController()
        {

        }
        public IEnumerable<string> Get()
        {

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5

        public string Get(int id)
        {
            System.Diagnostics.Debug.WriteLine("ffffffffffffffffffff");

            return "value";
        }

        // POST api/values
        public int Post(Type type)
        {
            System.Diagnostics.Debug.WriteLine("ffffffffffffffffffff");
            return 5;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
