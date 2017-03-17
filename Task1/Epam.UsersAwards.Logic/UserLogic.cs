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
        private IUserDao userDao;
        private IAwardDao awardDao;
        public UserLogic(IUserDao userDao, IAwardDao awardDao)
        {
            this.userDao = userDao;
            this.awardDao = awardDao;
        }

        public List<User> GetAll()
        {
            return userDao.GetAllUsers().ToList();
        }

        public User GetUserByID(int userID)
        {
            throw new NotImplementedException();
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
            if (userDao.Add(user) != null)
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
