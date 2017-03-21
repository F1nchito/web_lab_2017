﻿using Epam.UsersAwards.DalContracts;
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

        public PictureData GetPicture(int id)
        {
            return userDao.GetPicture(id);
        }

        public User GetUserByID(int userID)
        {
            return userDao.GetUserByID(userID);
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

        public bool SaveAwardToUser(string userID, string awardID)
        {
            throw new NotImplementedException();
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

        public bool userDelete(int userID)
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
