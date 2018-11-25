using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Arena.Models;
using System.Data;

namespace Arena
{
    public class DBHandler
    {
        DBManager dbMan;

        public DBHandler()
        {
            dbMan = new DBManager();
        }
        public DataTable check_signin(Signin s)
        {
            //  string query = "SELECT COUNT(*) FROM Player WHERE userName='" + s.username + "' AND password='" + s.password+"';";
            string query = "SELECT * FROM Player WHERE userName='" + s.username + "' AND password='" + s.password + "';";
            return dbMan.ExecuteReader(query);
        }
    }
}