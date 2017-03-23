using Epam.UsersAwards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.UsersAwards.LogicContracts
{
    public interface IUserLogic : IGeneralLogic<User>
    {
        User GetUserByID(int userID);
        bool SaveAwardToUser(int userID, int awardID);
        User GetUserByName(string name);
        List<User> GetUsersByFilter(string filter);
    }
}
