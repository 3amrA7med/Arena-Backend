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

        public DataTable clubOwner_maintanance(ClubOwner_maintanance c)
        {
            string query = "INSERT INTO Maintanance (clubid,pitch#,startTime,endTime,cost,description) " +
                " VALUES (" + c.clubid + "," + c.pitch_no + ",'" + c.maintanance_start_date.ToString("yyyy-MM-dd hh:mm:00.000")
                + "','" + c.maintanance_end_date.ToString("yyyy-MM-dd hh:mm:00.000") + "'," + c.cost + ",'" + c.description + "')";
            if (dbMan.ExecuteNonQuery(query) != 0)
            {
               
                    query = "SELECT * FROM Maintanance WHERE clubId=" + c.clubid + " AND Pitch#=" + c.pitch_no + ";";
                    return dbMan.ExecuteReader(query);
              
            }
            else return null;

        }

        public DataTable clubOwner_event(ClubOwner_event c)
        {
            string query = "Insert into Event (clubId,name,startTime,endTime,noOfTeams,noOfTeamMembers,availablePlaces,prize,pricePerTeam) " +
                " VALUES (" + c.clubid + ",'" + c.name + "','" + c.event_start_time.ToString("yyyy-MM-dd hh:mm:00.000")
                + "','" + c.event_end_time.ToString("yyyy-MM-dd hh:mm:00.000") + "'," + c.noofteams + "," + c.noofteammembers 
                + ","+c.availableplaces + ","+c.prize + ","+ c.priceperteam + ")";
            if (dbMan.ExecuteNonQuery(query) != 0)
            {

                query = "SELECT * FROM Event WHERE clubId=" + c.clubid + " AND startTime='" 
                    + c.event_start_time.ToString("yyyy-MM-dd hh:mm:00.000") + "' AND endTime='" 
                    + c.event_end_time.ToString("yyyy-MM-dd hh:mm:00.000") + "';";
                return dbMan.ExecuteReader(query);

            }
            else return null;

        }

        public DataTable clubOwner_pitch(ClubOwner_pitch c)
        {
            string query = "Insert into Pitch (clubId,pitch#,creationDate,capacity,price,type) " +
                " VALUES (" + c.clubid + "," + c.pitch_no + ",'" + c.creation_date.ToString("yyyy-MM-dd ")
                + "'," + c.capacity + "," + c.price + "," + c.type+ ")";
            if (dbMan.ExecuteNonQuery(query) != 0)
            {

                query = "SELECT * FROM Pitch WHERE clubId=" + c.clubid + " AND pitch# ="
                    + c.pitch_no + ";";
                return dbMan.ExecuteReader(query);

            }
            else return null;

        }

        public DataTable clubOwner_viewacademy(int clubid)
        {
            string query = "select A.name,A.monthlySubscription,(select count(P.username)" +
                " from Player P where P.clubId=A.clubId) as noofplayers from Academy A where A.clubId=" + clubid + ";";
            return dbMan.ExecuteReader(query);

        }

        public DataTable clubOwner_pitchno(int clubid)
        {
            string query = "select pitch# as pitchno from Pitch where clubId=" + clubid;
            return dbMan.ExecuteReader(query);

        }

        public DataTable clubOwner_academy(ClubOwner_academy c)
        {
            string query = "Update Academy set monthlySubscription =" + c.subscription + " where clubId ="+c.clubid;
            if (dbMan.ExecuteNonQuery(query) != 0)
            {

                query = "select *from Academy where clubid="+c.clubid+ " and monthlySubscription="+c.subscription;
                return dbMan.ExecuteReader(query);

            }
            else return null;

        }

        public DataTable clubOwner_addacademy(ClubOwner_addacademy c)
        {
            string query = "insert into Academy (name,monthlySubscription,clubId) values ('" + c.name + "',"+ c.subscription+","+c.clubid+")";
            if (dbMan.ExecuteNonQuery(query) != 0)
            {

                query = "select *from Academy where clubid=" + c.clubid + " and monthlySubscription=" + c.subscription;
                return dbMan.ExecuteReader(query);

            }
            else return null;

        }

        public DataTable clubOwner_getid(string username)
        {
            string query = "select id from Club where clubOwner='" + username + "';";
            return dbMan.ExecuteReader(query);
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
        public DataTable UpdatePlayer(Player p)
        {
            string query = "update Player set firstName = '" + p.fname + "' , lastName = '" + p.lname + "' , phone = '" + p.phone + "' , email = '" + p.email + "' , visa# = " + p.visa + " , password = '" + p.password + "' , birthDate = '" + p.bdate.ToString("yyyy-MM-dd") + "' where userName = '" + p.username + "'";
            if (dbMan.ExecuteNonQuery(query) != 0)
            {
                query = "SELECT *, 'player' AS type FROM Player WHERE userName='" + p.username + "' AND password='" + p.password + "';";
                return dbMan.ExecuteReader(query);
            }
            return null;
        }
        public DataTable GetMyAcademy(string username)
        {
            string query = "select C.name as club_name,C.city,C.street,P.academyName,A.monthlySubscription from Player P, Club C, Academy A where userName = '" + username + "' and P.clubId=C.id and P.academyName=A.name";
            return dbMan.ExecuteReader(query);
        }
        public DataTable Unsubscribe (Player p)
        {
            string query = "update Player set academyName ='NULL' , clubId='NULL' where userName = '" + p.username + "'";
            if (dbMan.ExecuteNonQuery(query) != 0)
            {
                query = "SELECT *, 'player' AS type FROM Player WHERE userName='" + p.username + "' AND password='" + p.password + "';";
                return dbMan.ExecuteReader(query);
            }
            return null;

        }
    }
}