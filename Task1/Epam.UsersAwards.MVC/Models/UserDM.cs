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
using System.Web.Helpers;
using System.Text;

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
                //TODO: webimage
                WebImage img = new WebImage(model.Photo.InputStream);
                img.Resize(250, 250);
                img.GetBytes();
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
        
        public byte[] GetAllAsFile()
        {
            var users = userLogic.GetAll();
            List<string> userStrings = new List<string>(users.Count);
            userStrings.Add($"List of all users: {Environment.NewLine}{Environment.NewLine}");
            foreach (var user in users)
            {
                string awards;
                if(user.Awards.Count == 0)
                {
                    awards = "no awards";
                }
                else
                {
                    awards = string.Join(", ", user.Awards.Select(award => award.Title));
                }
                userStrings.Add($"User with ID:{user.ID} and name {user.Name}{Environment.NewLine}" +
                    $"Date of birth: {user.DOB.ToShortDateString()}({user.Age}){Environment.NewLine}" +
                    $"with awards: {awards} {Environment.NewLine}{Environment.NewLine}");
            }
            byte[] userBytes = userStrings
                .SelectMany(s => Encoding.ASCII.GetBytes(s))
                .ToArray();
            return userBytes;
        }
    }
}