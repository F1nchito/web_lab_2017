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
            var users =  userDao.GetAllUsers().ToList();
            foreach (var user in users)
            {
                user.Awards = GetUserAwards(user);
            }
            return users;
        }

        public User GetUserByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }
            else
            {
                return userDao.GetUserByName(name);
            }
        }

        public List<User> GetUsersByFilter(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                return null;
            }
            else
            {
                return userDao.GetUserByFilter(filter).ToList();
            }
        }

        public PictureData GetPicture(int id)
        {
            return userDao.GetPicture(id);
        }

        public User GetUserByID(int userID)
        {
            var user =  userDao.GetUserByID(userID);
            user.Awards = GetUserAwards(user);
            return user;
        }

        public User Save(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Name))
            {
                return null;
            }
            if (user.DOB >= DateTime.Now | user.DOB <= DateTime.Now.AddYears(-150))
            {
                return null;
            }
            if (userDao.Add(user) != null)
            {
                return user;
            }
            throw new InvalidOperationException("Ошибка при сохранении");
        }

        public bool SaveAwardToUser(int userID, int awardID)
        {
            var user = userDao.GetUserByID(userID);
            var award = awardDao.GetAwardByID(awardID);
            if (user == null || award == null)
            {
                return false;
            }
            else if (user.Awards != null && user.Awards.Contains(award))
            {
                return false;
            }
            else
            {
                return userDao.AddAwardToUser(userID, awardID);
            }
        }

        public List<Award> GetUserAwards(User user)
        {
            return userDao.GetUserAwards(user).ToList();
        }

        public User Update(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Name))
            {
                return null;
            }
            if (user.DOB >= DateTime.Now | user.DOB <= DateTime.Now.AddYears(-150))
            {
                return null;
            }
            if (userDao.Update(user) != null)
            {
                return user;
            }
            return null;
            //throw new InvalidOperationException("Ошибка при сохранении");
        }

        public bool Delete(int userID)
        {
            if(userDao.GetUserByID(userID) != null)
            {
                return userDao.Delete(userID);
            }
            else
            {
                return false;
            }
            
        }

    }
}
