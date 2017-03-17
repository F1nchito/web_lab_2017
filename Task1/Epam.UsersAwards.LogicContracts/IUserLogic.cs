using Epam.UsersAwards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.UsersAwards.LogicContracts
{
    public interface IUserLogic
    {
        bool userDelete(int userID);
        List<User> GetAll();
        User GetUserByID(int userID);
        User Save(string userName, DateTime userDOB);
        bool SaveAwardToUser(string userID, string awardID);
        User Update(int ID, string Name, DateTime DOB);
    }
}
