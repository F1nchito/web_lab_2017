using Epam.UsersAwards.DalContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.UsersAwards.Entities;

namespace Epam.UsersAwards.DBDal
{
    public class DBUserDao : IUserDao
    {
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
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public User GetUserByID(int userID)
        {
            throw new NotImplementedException();
        }

        public User Update(int userID, string name, DateTime dob)
        {
            throw new NotImplementedException();
        }
    }
}
