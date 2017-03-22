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
        User Save(User user);
        bool SaveAwardToUser(int userID, int awardID);
        User Update(User user);
        PictureData GetPicture(int id);
        User GetUserByName(string name);
        List<User> GetUsersByFilter(string filter);
    }
}
