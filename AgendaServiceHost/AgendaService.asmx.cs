using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;

namespace CodeCamp.WP7.Service
{
    /// <summary>
    /// Summary description for AgendaService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AgendaService : System.Web.Services.WebService
    {
        private string connStr = ConfigurationManager.ConnectionStrings["OrlandoCodeCamp"].ConnectionString;

        [WebMethod]
        public List<AgendaItem> GetAgendaItems(string email, string password)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                try
                {
                    int personId = Login(conn, email, password);
                    return GetAgendaItems(conn, personId);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        [WebMethod]
        public void UpdateRating(string email, string password, AgendaItem item)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                try
                {
                    int personId = Login(conn, email, password);

                    string sql =
                        string.Format("delete from SessionAttendees where EventAttendeeId = {0} and SessionId = {1};", personId, item.SessionId) +
                        string.Format("insert into SessionAttendees (EventAttendeeId, CheckedIn, Rating, Comment, SessionId) values ({0}, 1, '{1}', '{2}', {3});", personId, item.Rating, item.Comment, item.SessionId);

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                        cmd.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        [WebMethod]
        public List<AgendaItem> Attend(string email, string password, int sessionId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                try
                {
                    int personId = Login(conn, email, password);

                    string sql =
                        string.Format("delete from SessionAttendees where EventAttendeeId = {0} and SessionId in (select Id from Sessions where StartTime = (select StartTime from Sessions where Id = {1}));", personId, sessionId) +
                        string.Format("insert into SessionAttendees (EventAttendeeId, CheckedIn, Rating, Comment, SessionId) values ({0}, 1, '', '', {1});", personId, sessionId);

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                        cmd.ExecuteNonQuery();

                    return GetAgendaItems(conn, personId);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        [WebMethod]
        public Event GetEvent(string email, string password)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                try
                {
                    Event e = new Event();
                    e.Email = email;
                    e.Password = password;

                    e.Tracks = GetTracks(conn);

                    e.Speakers = GetSpeakers(conn);

                    e.Sessions = GetSessions(conn);

                    try
                    {
                        int personId = Login(conn, email, password);

                        e.Agenda = GetAgendaItems(conn, personId);
                    }
                    catch
                    {
                        // do nothing
                    }

                    return e;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private List<Track> GetTracks(SqlConnection conn)
        {
            List<Track> tracks = new List<Track>();

            string sql = "select Name from Tracks order by Name";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    tracks.Add(new Track()
                    {
                        Name = (string)dr["Name"]
                    });
                }

                return tracks;
            }
        }

        private List<Speaker> GetSpeakers(SqlConnection conn)
        {
            List<Speaker> speakers = new List<Speaker>();

            string sql = "select distinct speaker.Name, speaker.Title, speaker.Bio, speaker.Blog, speaker.Twitter " +
                "from Sessions s " + 
                "join Tracks t on s.TrackId = t.Id " + 
                "join EventPresentations ep on s.EventPresentation_Id = ep.Id " +
                "join Presentations p on p.Id = ep.PresentationId " +
                "join PresentationSpeakers ps on ps.PresentationsAsSpeaker_Id = ep.PresentationId " +
                "join People speaker on speaker.Id = ps.Speakers_Id " +
                "order by speaker.Name;";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    speakers.Add(new Speaker()
                    {
                        Name = (string)dr["Name"],
                        Title = (string)dr["Title"],
                        Bio = (string)dr["Bio"],
                        Blog = (string)dr["Blog"],
                        Twitter = (string)dr["Twitter"]
                    });
                }

                return speakers;
            }
        }

        private List<Session> GetSessions(SqlConnection conn)
        {
            List<Session> sessions = new List<Session>();

            string sql = "select s.Id, s.Name, t.Name as Track, CONVERT(varchar(10), s.StartTime, 108) as StartTime, CONVERT(varchar(10), s.EndTime, 108) as EndTime, p.Level, r.Name as Room, s.Description, isnull(speaker.Name, '') as Speaker " +
                "from Sessions s " +
                "left join Tracks t on s.TrackId = t.Id " +
                "left join EventPresentations ep on s.EventPresentation_Id = ep.Id " +
                "left join Presentations p on p.Id = ep.PresentationId " +
                "left join PresentationSpeakers ps on ps.PresentationsAsSpeaker_Id = ep.PresentationId " +
                "left join People speaker on speaker.Id = ps.Speakers_Id " +
                "left join Rooms r on r.Id = s.RoomId " +
                "order by s.StartTime;";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    sessions.Add(new Session()
                    {
                        Id = (int)dr["Id"],
                        Name = (string)dr["Name"],
                        Track = (string)dr["Track"],
                        StartTime = (string)dr["StartTime"],
                        EndTime = (string)dr["EndTime"],
                        Level = (string)dr["Level"],
                        Room = (string)dr["Room"],
                        Description = (string)dr["Description"],
                        Speaker = (string)dr["Speaker"]
                    });
                }

                return sessions;
            }
        }

        private int Login(SqlConnection conn, string email, string password)
        {
            string hash = Encryption.ComputePasswordHash(password);
            string sql = string.Format("select id from people where email = '{0}' and passwordhash = '{1}';", email, hash);
            
            Debug.WriteLine(sql);

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (!dr.Read())
                    throw new Exception("login failed");

                return (int)dr["Id"];
            }
        }

        private List<AgendaItem> GetAgendaItems(SqlConnection conn, int personId)
        {
            var list = new List<AgendaItem>();

            string sql = string.Format("select SessionId, Rating, Comment " +
                "from SessionAttendees sa " +
                "join Sessions s on s.Id = sa.SessionId " +
                "where EventAttendeeId = {0} " +
                "order by s.StartTime", personId);

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            using (SqlDataReader dr = cmd.ExecuteReader())
                while (dr.Read())
                {
                    list.Add(new AgendaItem()
                    {
                        SessionId = (int)dr["SessionId"],
                        Rating = (string)dr["Rating"],
                        Comment = (string)dr["Comment"]
                    });
                }

            return list;
        }
    }
}