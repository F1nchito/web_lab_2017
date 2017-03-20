using Epam.UsersAwards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.UsersAwards.DalContracts
{
    public interface IUserDao
    {
        User Add(User user);
        User GetUserByID(int userID);
        IEnumerable<User> GetAllUsers();
        bool Delete(int userID);
        User Update(User user);
        bool AddAwardToUser(int userID, int awardID);
    }
}
