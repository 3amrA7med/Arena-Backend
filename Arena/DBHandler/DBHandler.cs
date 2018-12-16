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
        public DataTable insertBookingDB(PlayerBooking p)
        {
            string query = "INSERT INTO schedule VALUES ('" + p.date+" "+ p.date2 +":00', '"+p.paid+"', '"+p.unpaid+ "', '"+p.selclub+ "', '"+p.selpitchno+ "','"+p.user+"');";
            if (dbMan.ExecuteNonQuery(query) == 1)
                return dbMan.ExecuteReader("SELECT * FROM SCHEDULE WHERE startTime = '" + p.date + " " + p.date2 + ":00' AND clubId = '" + p.selclub + "' AND pitch# = '" + p.selpitchno + "';");
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
        public DataTable getAcadAllClubsDB()
        {
            string query = "select a.clubID as cid,c.name as cname,a.name as aname, monthlySubscription as price from academy as a, club as c where a.clubid = c.id;";
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

        public int DeleteEvent(int id,string username)
        {
            string query = "select Club.id from Club where Club.clubOwner ='"+username+"'";
            Object oid = dbMan.ExecuteScalar(query);
            int clubid = (int)oid;
             query = "DELETE FROM EVENT "
                + "WHERE EVENT.clubId='" + clubid + "' And EVENT.eventId=" + id + ";";
           return dbMan.ExecuteNonQuery(query);
        }

        public int DeleteReservation(DateTime date,int num, string username, string time)
        {
            string query = "select Club.id from Club where Club.clubOwner ='" + username + "'";
            Object oid = dbMan.ExecuteScalar(query);
            int clubid = (int)oid;
            query = "DELETE FROM SCHEDULE "
                + "WHERE SCHEDULE.clubId='" + clubid + "' And SCHEDULE.startTime='" + date.ToString("yyyy - MM - dd") + " "+time+ ":00' AND "
                + " SCHEDULE.pitch#='" + num + "';";
            return dbMan.ExecuteNonQuery(query);
        }

        public int DeleteMaint(DateTime date, int num, string username, string time,string minutes)
        {
            string query = "select Club.id from Club where Club.clubOwner ='" + username + "'";
            Object oid = dbMan.ExecuteScalar(query);
            int clubid = (int)oid;
            query = "DELETE FROM Maintanance "
                + "WHERE  Maintanance.clubId='" + clubid + "' And Maintanance.startTime='" + date.ToString("yyyy - MM - dd") +" "+time+":"+minutes+
                "' AND "
                + " Maintanance.pitch#='" + num + "';";
           return dbMan.ExecuteNonQuery(query);
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

        public DataTable clubOwner_viewacademy(ClubOwner_clubid c)
        {
            string query = "select A.name,A.monthlySubscription,(select count(P.username)" +
                " from Player P where P.clubId=A.clubId) as noofplayers from Academy A where A.clubId=" + c.clubid + ";";
            return dbMan.ExecuteReader(query);

        }

        public DataTable clubOwner_pitchno(ClubOwner_clubid c)
        {
            string query = "select pitch# as pitchno from Pitch where clubId=" + c.clubid;
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

        public DataTable clubOwner_getid(ClubOwner_username u)
        {
            string query = "select id from Club where clubOwner='" + u.username + "';";
            return dbMan.ExecuteReader(query);
        }

        public DataTable GetEvents(DateTime date,string username)
        {

            string query = "SELECT *  FROM EVENT  JOIN CLUB ON EVENT.clubId=CLUB.id "
                + "WHERE EVENT.startTime between '" + date.ToString("yyyy-MM-dd") + "'and '" + date.ToString("yyyy-MM-dd") + " 23:59'"
                + " AND CLUB.clubOwner='"+username+"'";
            return dbMan.ExecuteReader(query);
        }


        public DataTable GetReservations(DateTime date, string username)
        {

            string query = "SELECT *  FROM Schedule  JOIN CLUB ON Schedule.clubId=CLUB.id "
                + "WHERE Schedule.startTime between '" + date.ToString("yyyy-MM-dd") + "'and '" + date.ToString("yyyy-MM-dd") + " 23:59'" 
                + " AND CLUB.clubOwner='" + username + "'";
            return dbMan.ExecuteReader(query);
        }

        public DataTable GetMaint(DateTime date, string username)
        {

            string query = "SELECT *  FROM Maintanance  JOIN CLUB ON Maintanance.clubId=CLUB.id "
                + "WHERE Maintanance.startTime between '" + date.ToString("yyyy-MM-dd") + "'and '" + date.ToString("yyyy-MM-dd") + " 23:59'"
                + " AND CLUB.clubOwner='" + username + "'";
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
            string query = "select C.name as club_name,C.city,C.street,P.academyName,A.monthlySubscription from Player P, Club C, Academy A where userName = '" + username + "' and P.clubId=C.id and P.academyName=A.name and A.clubId = C.id";
            return dbMan.ExecuteReader(query);
        }
        public DataTable Unsubscribe (Player p)
        {
            string query = "update Player set academyName =NULL , clubId=NULL where userName = '" + p.username + "'";
            if (dbMan.ExecuteNonQuery(query) != 0)
            {
                query = "SELECT *, 'player' AS type FROM Player WHERE userName='" + p.username + "' AND password='" + p.password + "';";
                return dbMan.ExecuteReader(query);
            }
            return null;

        }
        public DataTable SubscribeDB(Player_Subscription p)
        {
            string query = "update Player set academyName ='"+p.aname+"' , clubId='"+p.cid+"' where userName = '" + p.username + "'";
            if (dbMan.ExecuteNonQuery(query) != 0)
            {
                query = "SELECT *, 'player' AS type FROM Player WHERE userName='" + p.username + "' AND academyName='" + p.aname + "';";
                return dbMan.ExecuteReader(query);
            }
            return null;

        }
        public DataTable GetPastEvents(string username)
        {
            string query = "select C.name as clubName, C.street, C.city, E.name, E.startTime from Event E, Club C, Participate P where P.playerUserName = '" + username + "' and P.eventId = E.eventId and P.clubId = E.clubId and E.clubId = C.id and E.startTime < convert(date, getdate())";
            return dbMan.ExecuteReader(query);
        }
        public DataTable GetAvailableEvents(string username,string city)
        {
            string query = "select c.name as cname,e.name as ename,eventId as eid, e.clubId as cid,availablePlaces as ap,convert(char,startTime) as st,priceperteam as ppt, prize as pz, noofteams as noot, noofteammembers as nootm, convert(char,endtime) as et from event as e, club as c,Player as u where c.city = '" + city + "' and c.id = e.clubId and  '" + username + "' not in (select username from participate as p where p.clubid = c.id and p.eventId = e.eventId) and availablePlaces >0 and e.startTime > convert(date, getdate());";
            return dbMan.ExecuteReader(query);
        }
        public DataTable enrollEvent(Player_Enrollment p)
        {
            int cid = p.cid;
            int eid = p.eid;
            string username = p.username;
            string query = "select availableplaces from event where eventId = '" + eid + "'and clubId = '" + cid + "'";
            int available = (int)dbMan.ExecuteScalar(query);
            if (available > 0)
            {
                query = "insert into Participate values('" + username + "', '" + eid + "', '" + cid + "')";
                if (dbMan.ExecuteNonQuery(query) == 1)
                {
                    available--;
                    query = "update event set availableplaces = '"+available+"' where clubId = '"+cid+"' and eventId = '"+eid+"';";
                    dbMan.ExecuteNonQuery(query);
                    query = "select * from Participate where clubId = '" + cid + "' and eventId = '" + eid + "' and playerUserName = '" + username + "';";
                    return dbMan.ExecuteReader(query);
                }
                else return null;
                
            }
                
            else return null;
        }
        public DataTable GetPastReservations(string username)
        {
            string query = "select * from Schedule where playerUserName = '" + username + "' and startTime < convert (date,getdate())";
            return dbMan.ExecuteReader(query);
        }
        public DataTable GetUpcomingEvents(string username)
        {
            string query = "select C.name as clubName, C.street, C.city, E.name, E.startTime from Event E, Club C, Participate P where P.playerUserName = '" + username + "' and P.eventId = E.eventId and P.clubId = E.clubId and E.clubId = C.id and E.startTime > convert(date, getdate())";
            return dbMan.ExecuteReader(query);
        }
        public DataTable GetUpcomingReservations(string username)
        {
            string query = "select * from Schedule where playerUserName = '" + username + "' and startTime > convert (date,getdate())";
            return dbMan.ExecuteReader(query);
        }
    
    }
}