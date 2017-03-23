using Epam.UsersAwards.DalContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.UsersAwards.Entities;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Epam.UsersAwards.DBDal
{
    public class DBAwardDao : IAwardDao
    {
        public readonly string dbConStr;
        public DBAwardDao()
        {
            dbConStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        }

        public IEnumerable<Award> GetAllAwards()
        {
            using (var con = new SqlConnection(dbConStr))
            {
                //var cmd = con.CreateCommand();
                //cmd.CommandText = "AwardGetAll";
                var cmd = new SqlCommand("AwardGetAll", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                var result = cmd.ExecuteReader();
                while (result.Read())
                {
                    int id = (int)result["ID"];
                    string title = (string)result["Title"];
                    string description = (string)result["Description"];
                    yield return new Award() {ID =id, Title = title, Description = description};
                }
            }
        }

        public Award GetAwardByID(int awardID)
        {
            using (var con = new SqlConnection(dbConStr))
            {
                //var cmd = con.CreateCommand();
                //cmd.CommandText = "AwardGetByID";
                var cmd = new SqlCommand("AwardGetByID", con);
                cmd.Parameters.AddWithValue("@ID", awardID);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                var result = cmd.ExecuteReader();
                if (result.Read())
                {
                    int id = (int)result["ID"];
                    string title = (string)result["Title"];
                    string description = (string)result["Description"];
                    return new Award() { ID = id, Title = title, Description = description};
                }
                else
                {
                    return null;
                }
            }
        }

        public Award GetAwardByName(string name)
        {
            using (var con = new SqlConnection(dbConStr))
            {
                var cmd = new SqlCommand("AwardGetByName", con);
                cmd.Parameters.AddWithValue("@Title", name);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                var result = cmd.ExecuteReader();
                result.Read();
                try
                {
                    int id = (int)result["ID"];
                    string title = (string)result["Title"];
                    string description = (string)result["Description"];
                    var award = new Award() { ID = id, Title = title, Description = description };
                    return award;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public IEnumerable<Award> GetAwardsByFilter(string filter)
        {
            using (var con = new SqlConnection(dbConStr))
            {
                var cmd = con.CreateCommand();
                if (filter.Length == 1)
                {
                    cmd.CommandText = "AwardGetAllByChar";
                }
                else
                {
                    cmd.CommandText = "AwardGetAllByFilter";
                }
                cmd.Parameters.AddWithValue("@Filter", filter);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                var result = cmd.ExecuteReader();
                while (result.Read())
                {
                    int id = (int)result["ID"];
                    string title = (string)result["Title"];
                    string description = (string)result["Description"];
                    var award = new Award() { ID = id, Title = title, Description = description };
                    yield return award;
                }
            }
        }

        public Award Add(Award award)
        {
            using (var con = new SqlConnection(dbConStr))
            {
                //var cmd = connection.CreateCommand();
                //cmd.CommandText = "AwardAdd";
                var cmd = new SqlCommand("AwardAdd", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Title", award.Title);
                cmd.Parameters.AddWithValue("@Description", award.Description);
                if (award.Image != null)
                {
                    var picID = SavePicture(award.Image);
                    cmd.Parameters.AddWithValue("@picID", picID);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@picID", DBNull.Value);
                }
                con.Open();
                award.ID = (int)(decimal)cmd.ExecuteScalar();
                return award ;
            }
        }

        public bool Delete(int awardID)
        {
            using (var connection = new SqlConnection(dbConStr))
            {
                //var cmd = connection.CreateCommand();
                //cmd.CommandText = "AwardDelete";
                var cmd = new SqlCommand("AwardDelete", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", awardID);
                connection.Open();
                return (cmd.ExecuteNonQuery() >= 1);
            }
        }

        public PictureData GetPicture(int id)
        {
            using (var con = new SqlConnection(dbConStr))
            {
                //var cmd = con.CreateCommand();
                //cmd.CommandText = "AwardPictureGetByID";
                var cmd = new SqlCommand("AwardPictureGetByID", con);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                var result = cmd.ExecuteReader();
                result.Read();
                try//или все таки if()?
                {
                    return new PictureData() { Data = (byte[])result["Data"], ContentType = (string)result["Type"] };
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public bool UpdatePicture(int userID, PictureData picture)
        {
            try
            {
                using (var connection = new SqlConnection(dbConStr))
                {
                    //var cmd = connection.CreateCommand();
                    //cmd.CommandText = "AwardPictureUpdate";
                    var cmd = new SqlCommand("AwardPictureUpdate", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@awardID", userID);
                    cmd.Parameters.AddWithValue("@Data", picture.Data);
                    cmd.Parameters.AddWithValue("@Type", picture.ContentType);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int SavePicture(PictureData picture)
        {
            using (var connection = new SqlConnection(dbConStr))
            {
                //var cmd = connection.CreateCommand();
                //cmd.CommandText = "AwardPictureAdd";
                var cmd = new SqlCommand("AwardPictureAdd", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Data", picture.Data);
                cmd.Parameters.AddWithValue("@Type", picture.ContentType);
                connection.Open();
                return (int)(decimal)cmd.ExecuteScalar();
            }
        }

        public Award Update(Award award)
        {
            if(award.Image != null)
            {
                UpdatePicture(award.ID, award.Image);
            }
            using (var con = new SqlConnection(dbConStr))
            {
                //var cmd = con.CreateCommand();
                //cmd.CommandText = "AwardUpdate";
                var cmd = new SqlCommand("AwardUpdate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    cmd.Parameters.AddWithValue("@ID", award.ID);
                    cmd.Parameters.AddWithValue("@Title", award.Title);
                    cmd.Parameters.AddWithValue("@Description", award.Description);
                    con.Open();
                    if (cmd.ExecuteNonQuery() >= 1)
                    {
                        return award;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

    }
}
