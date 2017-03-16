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
    public class DBUserDao : IUserDao
    {
        public readonly string dbConStr;
        public DBUserDao()
        {
            dbConStr = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;//вынести в settings
        }
        public User Add(User user)
        {
            throw new NotImplementedException();
        }

        public bool AddAwardToUser(int userID, int awardID)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int userID)
        {
            using (var connection = new SqlConnection(dbConStr))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = "UserDelete";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", userID);
                connection.Open();
                return (cmd.ExecuteNonQuery() >= 1);
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            using (var con = new SqlConnection(dbConStr))
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "UserGetAll";
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                var result = cmd.ExecuteReader();
                while (result.Read())
                {
                    int id = Int32.Parse((string)result["ID"]);
                    string name = (string)result["Name"];
                    DateTime dob = Convert.ToDateTime(result["DOB"], Settings.Settings.myCulture);
                    var user = new User(id, name, dob);
                    user.Awards = GetUserAwards(user).ToArray();
                    yield return user;
                }
            }
        }

        public User GetUserByID(int userID)
        {
            using (var con = new SqlConnection(dbConStr))
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "UserGetByID";
                cmd.Parameters.AddWithValue("@ID", userID);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                var result = cmd.ExecuteReader();
                result.Read();
                try//или все таки if()?
                {
                    int id = Int32.Parse((string)result["ID"]);
                    string name = (string)result["Name"];
                    DateTime dob = Convert.ToDateTime(result["DOB"], Settings.Settings.myCulture);
                    var user = new User(id, name, dob);
                    user.Awards = GetUserAwards(user).ToArray();
                    return user;
                }
                catch (Exception)
                {

                    return null;
                }
            }
        }

        public User Update(int userID, string name, DateTime dob)
        {
            using (var con = new SqlConnection(dbConStr))
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "UserUpdate";
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    cmd.Parameters.AddWithValue("@ID", userID);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@DOB", dob);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    return new User(userID, name, dob);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
