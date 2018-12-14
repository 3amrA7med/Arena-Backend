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
        //============================AMR FUNCTIONS======================================================================
        public DataTable check_signin(Signin s)
        {
            //  string query = "SELECT COUNT(*) FROM Player WHERE userName='" + s.username + "' AND password='" + s.password+"';";
            string query = "SELECT *, 'player' AS type FROM Player WHERE userName='" + s.username + "' AND password='" + s.password + "';";
            if (dbMan.ExecuteReader(query) == null)
                query = "SELECT *, 'owner' AS type FROM clubOwner WHERE userName='" + s.username + "' AND password='" + s.password + "';";
            return dbMan.ExecuteReader(query);
        }
        //string username,string password,string fname,string lname,string email,double phone,double visa,DateTime bdate
        public DataTable player_signup(Player p)
        {
            string query = "INSERT INTO Player (userName,password,firstName,lastName,email,phone,visa#,birthdate) " +
                " VALUES ('" + p.username + "','" + p.password + "','" + p.fname + "','" + p.lname + "','" +
                p.email + "'," + p.phone + "," + p.visa + ",'" + p.bdate.ToString("yyyy-MM-dd") + "')";
            if (dbMan.ExecuteNonQuery(query) != 0)
            {
                query = "SELECT *, 'player' AS type FROM Player WHERE userName='" + p.username + "' AND password='" + p.password + "';";
                return dbMan.ExecuteReader(query);
            }
            return null;
        }
        public DataTable availableSchedule(DateTime date, int cid, int pid)
        {
            //string query = "SELECT *  FROM EVENT  JOIN CLUB ON EVENT.clubId=CLUB.id "
            //  + "WHERE EVENT.startTime='" + date.ToString("yyyy-MM-dd HH:00") + "' AND CLUB.clubOwner='" + username + "'";
            string query = "select startTime from schedule where startTime between '" + date.ToString("yyyy-MM-dd") + "'and '" + date.ToString("yyyy-MM-dd") + " 23:00' and clubid = '"+cid+"' and pitch# = '"+pid+"';";
            return dbMan.ExecuteReader(query);
        }
        public DataTable getCitiesBooking()
        {
            string query = "select distinct city from club";
            return dbMan.ExecuteReader(query);
        }
        public DataTable insertBookingDB(string user, int selclub, int selpitchno, string date, int paid,int unpaid)
        {
            string query = "INSERT INTO schedule VALUES ('" + date+"', '"+paid+"', '"+unpaid+ "', '"+selclub+ "', '"+selpitchno+ "','"+user+"');";
            if (dbMan.ExecuteNonQuery(query) == 1)
                return dbMan.ExecuteReader("SELECT * FROM SCHEDULE WHERE startTime = '" + date + "' AND clubId = '" + selclub + "' AND pitch# = '" + selpitchno + "';");
            else return null;
  
        }
        public DataTable getPitchesDB(int id)
        {
            string query = "select pitch#,price from pitch where clubId = '"+id+"';";
            return dbMan.ExecuteReader(query);
        }
        public DataTable getClubsDB(string city)
        {
            string query = "select name,id from club where city = '"+city+"';" ;
            return dbMan.ExecuteReader(query);
        }

        public DataTable clubOwner_signup(ClubOwner_signup c)
        {
            string query = "INSERT INTO clubOwner (userName,password,firstName,lastName,email,phone,officeHours) " +
                " VALUES ('" + c.username + "','" + c.password + "','" + c.fname
                + "','" + c.lname + "','" + c.email + "'," + c.phone + ",'" + (c.officeHours == null ? "NULL" : c.officeHours) + "')";
            if (dbMan.ExecuteNonQuery(query) != 0)
            {
                query = "INSERT INTO CLUB(name,city,street,clubOwner) "
                    + "VALUES ('" + c.clubName + "','" + c.clubCity + "','" + c.clubStreet
                + "','" + c.username + "')";
                if (dbMan.ExecuteNonQuery(query) != 0)
                {
                    query = "SELECT *, 'owner' AS type FROM clubOwner WHERE userName='" + c.username + "' AND password='" + c.password + "';";
                    return dbMan.ExecuteReader(query);
                }
                else
                    return null;
            }
            else return null;

        }

        public DataTable GetEvents(DateTime date,string username)
        {

            string query = "SELECT *  FROM EVENT  JOIN CLUB ON EVENT.clubId=CLUB.id "
                +"WHERE EVENT.startTime='"+ date.ToString("yyyy-MM-dd") + "' AND CLUB.clubOwner='"+username+"'";
            return dbMan.ExecuteReader(query);
        }


        public DataTable GetReservations(DateTime date, string username)
        {

            string query = "SELECT *  FROM Schedule  JOIN CLUB ON Schedule.clubId=CLUB.id "
                + "WHERE Schedule.startTime='" + date.ToString("yyyy-MM-dd") + "' AND CLUB.clubOwner='" + username + "'";
            return dbMan.ExecuteReader(query);
        }

        public DataTable GetMaint(DateTime date, string username)
        {

            string query = "SELECT *  FROM Maintanance  JOIN CLUB ON Maintanance.clubId=CLUB.id "
                + "WHERE Maintanance.startTime='" + date.ToString("yyyy-MM-dd") + "' AND CLUB.clubOwner='" + username + "'";
            return dbMan.ExecuteReader(query);
        }

        //===========================================================================
    }
}