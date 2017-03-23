using Epam.UsersAwards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.UsersAwards.DalContracts
{
    public interface IUserDao : IGeneralDao<User>
    {
        User GetUserByID(int userID);
        IEnumerable<User> GetAllUsers();
        bool AddAwardToUser(int userID, int awardID);
        IEnumerable<Award> GetUserAwards(User user);
        User GetUserByName(string name);
        IEnumerable<User> GetUserByFilter(string filter);
    }
}
