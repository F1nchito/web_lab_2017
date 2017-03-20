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
                    yield return new Award() {ID =id, Title = title, Description = description };
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
                    return new Award() { ID = id, Title = title, Description = description};
                }
                else
                {
                    return null;
                }
            }
        }

        public Award Update(Award award)
        {
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
