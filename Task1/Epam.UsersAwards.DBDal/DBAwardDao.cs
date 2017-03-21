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
        public Award Add(Award award)
        {
            using (var connection = new SqlConnection(dbConStr))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = "AwardAdd";
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
                connection.Open();
                award.ID = (int)(decimal)cmd.ExecuteScalar();
                return award ;
            }
        }

        public bool Delete(int awardID)
        {
            using (var connection = new SqlConnection(dbConStr))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = "AwardDelete";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", awardID);
                connection.Open();
                return (cmd.ExecuteNonQuery() >= 1);
            }
        }

        public IEnumerable<Award> GetAllAwards()
        {
            using (var con = new SqlConnection(dbConStr))
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "AwardGetAll";
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                var result = cmd.ExecuteReader();
                while (result.Read())
                {
                    int id = (int)result["ID"];
                    string title = (string)result["Title"];
                    string description = (string)result["Description"];
                    yield return new Award() {ID =id, Title = title, Description = description, Image = GetPicture(id) };
                }
            }
        }

        public Award GetAwardByID(int awardID)
        {
            using (var con = new SqlConnection(dbConStr))
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "AwardGetByID";
                cmd.Parameters.AddWithValue("@ID", awardID);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                var result = cmd.ExecuteReader();
                if (result.Read())
                {
                    int id = (int)result["ID"];
                    string title = (string)result["Title"];
                    string description = (string)result["Description"];
                    return new Award() { ID = id, Title = title, Description = description, Image = GetPicture(id)};
                }
                else
                {
                    return null;
                }
            }
        }

        public PictureData GetPicture(int id)
        {
            using (var con = new SqlConnection(dbConStr))
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "AwardPictureGetByID";
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
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "AwardPictureUpdate";
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
                var cmd = connection.CreateCommand();
                cmd.CommandText = "AwardPictureAdd";
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
                var cmd = con.CreateCommand();
                cmd.CommandText = "AwardUpdate";
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
