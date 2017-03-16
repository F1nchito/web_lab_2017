using Epam.UsersAwards.DalContracts;
using Epam.UsersAwards.LogicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.UsersAwards.Entities;

namespace Epam.UsersAwards.Logic
{
    public class UserLogic : IUserLogic
    {
        public IUserDao userDao;
        private IAwardDao awardDao;
        public UserLogic()
        {
            userDao = DaoProvider.UserDao;
            awardDao = DaoProvider.AwardDao;
        }

        public User[] GetAll()
        {
            return userDao.GetAllUsers().ToArray();
        }

        public User Save(string userName, DateTime userDOB)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return null;
            }
            if (userDOB >= DateTime.Now | userDOB <= DateTime.Now.AddYears(-150))
            {
                return null;
            }
            User user = new User(userName, userDOB);
            if (userDao.Add(user))
            {
                return user;
            }
            throw new InvalidOperationException("Ошибка при сохранении");
        }

        public bool SaveAwardToUser(string userID, string awardID)
        {
            throw new NotImplementedException();
        }

        public User Update(int ID, string Name, DateTime DOB)
        {
            throw new NotImplementedException();
        }

        public bool userDelete(int userID)
        {
            throw new NotImplementedException();
        }
    }
}
