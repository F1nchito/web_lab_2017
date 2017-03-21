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
            dbConStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;//вынести в settings
        }
        public User Add(User user)
        {
            using (var connection = new SqlConnection(dbConStr))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = "UserAdd";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@DOB", user.DOB);
                if (user.Photo != null)
                {
                    var picID = SavePicture(user.Photo);
                    cmd.Parameters.AddWithValue("@picID", picID);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@picID", DBNull.Value);
                }
                connection.Open();
                user.ID = (int)(decimal)cmd.ExecuteScalar();
                return user;
            }
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
                    int id = (int)result["ID"];
                    string name = (string)result["Name"];
                    DateTime dob = Convert.ToDateTime(result["DOB"]);
                    var user = new User() {ID=id, Name=name, DOB=dob, Photo = GetPicture(id) };
                    //user.Awards = GetUserAwards(user).ToArray();
                    yield return user;
                }
            }
        }

        public PictureData GetPicture(int id)
        {
            using (var con = new SqlConnection(dbConStr))
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "UserPictureGetByID";
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

        public int SavePicture(PictureData picture)
        {
            using (var connection = new SqlConnection(dbConStr))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = "UserPictureAdd";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Data", picture.Data);
                cmd.Parameters.AddWithValue("@Type", picture.ContentType);
                connection.Open();
                return (int)(decimal)cmd.ExecuteScalar();
            }
        }

        public bool UpdatePicture(int userID,PictureData picture)
        {
            try
            {
                using (var connection = new SqlConnection(dbConStr))
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "UserPictureUpdate";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userID", userID);
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
                    int id = (int)result["ID"];
                    string name = (string)result["Name"];
                    DateTime dob = Convert.ToDateTime(result["DOB"]);
                    var user = new User() { ID = id, Name = name, DOB = dob, Photo = GetPicture(id) };
                    //user.Awards = GetUserAwards(user).ToArray();
                    return user;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public User Update(User user)
        {
            if (user.Photo != null)
            {
                UpdatePicture(user.ID, user.Photo);
            }
            using (var con = new SqlConnection(dbConStr))
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "UserUpdate";
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    cmd.Parameters.AddWithValue("@ID", user.ID);
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@DOB", user.DOB);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    return user;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
