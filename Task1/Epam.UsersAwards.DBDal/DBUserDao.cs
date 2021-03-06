﻿using Epam.UsersAwards.DalContracts;
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
        private IAwardDao awardDAO;
        public DBUserDao()
        {
            dbConStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            awardDAO = new DBAwardDao();
        }
        public User Add(User user)
        {
            using (var connection = new SqlConnection(dbConStr))
            {
                var cmd = new SqlCommand("UserAdd", connection);
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
            using (var connection = new SqlConnection(dbConStr))
            {
                var cmd = new SqlCommand("UserAddAward", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@uID", userID);
                cmd.Parameters.AddWithValue("@aID", awardID);
                connection.Open();
                return (cmd.ExecuteNonQuery() >= 1);
            }
        }

        public IEnumerable<Award> GetUserAwards(User user)
        {
            using (var con = new SqlConnection(dbConStr))
            {
                var cmd = new SqlCommand("UserGetAwards", con);
                cmd.Parameters.AddWithValue("@userID", user.ID);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                var result = cmd.ExecuteReader();
                while (result.Read())
                {
                    int id = (int)result["ID"];
                    string title = (string)result["Title"];
                    string description = (string)result["Description"];
                    yield return new Award()
                    {
                        ID = id,
                        Title = title,
                        Description = description,
                    };
                }
            }
        }

        public bool Delete(int userID)
        {
            using (var connection = new SqlConnection(dbConStr))
            {
                var cmd = new SqlCommand("UserDelete", connection);
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
                var cmd = new SqlCommand("UserGetAll", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                var result = cmd.ExecuteReader();
                while (result.Read())
                {
                    int id = (int)result["ID"];
                    string name = (string)result["Name"];
                    DateTime dob = Convert.ToDateTime(result["DOB"]);
                    var user = new User() {ID=id, Name=name, DOB=dob };
                    //user.Awards = GetUserAwards(user).ToArray();
                    yield return user;
                }
            }
        }

        public PictureData GetPicture(int id)
        {
            using (var con = new SqlConnection(dbConStr))
            {
                var cmd = new SqlCommand("UserPictureGetByID", con);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                var result = cmd.ExecuteReader();
                if(result.Read())
                {
                    return new PictureData() { Data = (byte[])result["Data"], ContentType = (string)result["Type"] };
                }
                else 
                {
                    return null;
                }
            }
        }

        public int SavePicture(PictureData picture)
        {
            using (var connection = new SqlConnection(dbConStr))
            {
                var cmd = new SqlCommand("UserPictureAdd", connection);
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
                    var cmd = new SqlCommand("UserPictureUpdate", connection);
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
                var cmd = new SqlCommand("UserGetByID", con);
                cmd.Parameters.AddWithValue("@ID", userID);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                var result = cmd.ExecuteReader();
                if(result.Read())
                {
                    int id = (int)result["ID"];
                    string name = (string)result["Name"];
                    DateTime dob = Convert.ToDateTime(result["DOB"]);
                    var user = new User() { ID = id, Name = name, DOB = dob };
                    //user.Awards = GetUserAwards(user).ToArray();
                    return user;
                }
                else
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
                var cmd = new SqlCommand("UserUpdate", con);
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

        public User GetUserByName(string name)
        {
            using (var con = new SqlConnection(dbConStr))
            {
                var cmd = new SqlCommand("UserGetByName", con);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                var result = cmd.ExecuteReader();
                if(result.Read())
                {
                    int id = (int)result["ID"];
                    string nameDB = (string)result["Name"];
                    DateTime dob = Convert.ToDateTime(result["DOB"]);
                    var user = new User() { ID = id, Name = nameDB, DOB = dob };
                    //user.Awards = GetUserAwards(user).ToArray();
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }

        public IEnumerable<User> GetUserByFilter(string filter)
        {
            using (var con = new SqlConnection(dbConStr))
            {
                var cmd = con.CreateCommand();
                if (filter.Length == 1)
                {
                    cmd.CommandText = "UserGetAllByChar";
                }
                else
                {
                    cmd.CommandText = "UserGetAllByFilter";
                }
                cmd.Parameters.AddWithValue("@Filter", filter);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                var result = cmd.ExecuteReader();
                while (result.Read())
                {
                    int id = (int)result["ID"];
                    string name = (string)result["Name"];
                    DateTime dob = Convert.ToDateTime(result["DOB"]);
                    var user = new User() { ID = id, Name = name, DOB = dob };
                    //user.Awards = GetUserAwards(user).ToArray();
                    yield return user;
                }
            }
        }
    }
}
