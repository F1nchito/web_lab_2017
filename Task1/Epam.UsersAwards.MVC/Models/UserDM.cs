using Epam.UsersAwards.LogicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Epam.UsersAwards.MVC.ViewModels;
using Epam.UsersAwards.MVC.ViewModels.Users;
using Epam.UsersAwards.Entities;
using AutoMapper;
using System.Web.Mvc;
using System.IO;

namespace Epam.UsersAwards.MVC.Models
{
    public class UserDM
    {
        private IUserLogic userLogic;
        public UserDM(IUserLogic userLogic)
        {
            this.userLogic = userLogic;
        }

        internal List<User> GetAll()
        {
            return userLogic.GetAll();
        }

        public UserEditVM GetUserForEdit(int id)
        {
            var user = userLogic.GetUserByID(id);
            return  Mapper.Map<UserEditVM>(user);
        }

        public User GetUserByName(string name)
        {
            var replacedName = name.Replace('_', ' ');
            return userLogic.GetUserByName(replacedName);
        }

        public List<User> GetUsersByFilter(string filter)
        {
            return userLogic.GetUsersByFilter(filter);
        }

        internal bool Save(UserCreateVM model)
        {
            var user = Mapper.Map<User>(model);
            if (model.Photo != null)
            {
                user.Photo = new PictureData();
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    model.Photo.InputStream.CopyTo(memoryStream);
                    user.Photo.Data = memoryStream.ToArray();
                    user.Photo.ContentType = model.Photo.ContentType;
                }
            }
            return (userLogic.Save(user) != null);
        }

        internal User Edit(UserEditVM model)
        {
            User user = Mapper.Map<User>(model);
            if (model.Photo != null)
            {
                user.Photo = new PictureData();
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    model.Photo.InputStream.CopyTo(memoryStream);
                    user.Photo.Data = memoryStream.ToArray();
                    user.Photo.ContentType = model.Photo.ContentType;
                }
            }
            return userLogic.Update(user);
        }

        internal PictureData GetPicture(int id)
        {
            return userLogic.GetPicture(id);
        }

        internal bool Delete(int id)
        {
            return userLogic.Delete(id);
        }

        internal bool AddAwardToUser(int userID, int awardID)
        {
            return userLogic.SaveAwardToUser(userID, awardID);
        }
    }
}