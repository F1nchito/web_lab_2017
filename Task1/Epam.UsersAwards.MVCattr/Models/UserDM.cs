using Epam.UsersAwards.LogicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Epam.UsersAwards.Entities;
using AutoMapper;
using System.Web.Mvc;
using System.IO;
using System.Web.Helpers;
using System.Text;
using System.Configuration;
using Epam.UsersAwards.MVCattr.ViewModels;
using Epam.UsersAwards.MVCattr.ViewModels.Users;

namespace Epam.UsersAwards.MVCattr.Models
{
    public class UserDM
    {
        private IUserLogic userLogic;
        public int imageHeight;
        public int imageWidth;
        public UserDM(IUserLogic userLogic)
        {
            this.userLogic = userLogic;
            imageHeight = Convert.ToInt32(ConfigurationManager.AppSettings.Get("imageHeight"));
            imageWidth = Convert.ToInt32(ConfigurationManager.AppSettings.Get("imageWidth"));
        }

        internal List<UserShowWithAwardsVM> GetAll()
        {
            var users =  userLogic.GetAll();
            return Mapper.Map<List<UserShowWithAwardsVM>>(users);
        }

        public UserShowWithAwardsVM GetUserByID(int id)
        {
            var user = userLogic.GetUserByID(id);
            return Mapper.Map<UserShowWithAwardsVM>(user);
        }

        public UserShowWithAwardsVM GetUserByName(string name)
        {
            var replacedName = name.Replace('_', ' ');
            var user = userLogic.GetUserByName(replacedName);
            return Mapper.Map<UserShowWithAwardsVM>(user);
        }

        public List<UserShowWithAwardsVM> GetUsersByFilter(string filter)
        {
            var users =  userLogic.GetUsersByFilter(filter);
            return Mapper.Map<List<UserShowWithAwardsVM>>(users);
        }

        internal User Save(UserCreateVM model)
        {
            var user = Mapper.Map<User>(model);
            if (model.Photo != null)
            {
                //TODO: webimage
                WebImage img = new WebImage(model.Photo.InputStream);
                img.Resize(imageWidth, imageHeight);
                user.Photo = new PictureData();
                user.Photo.Data = img.GetBytes();
                user.Photo.ContentType = "image/" + img.ImageFormat;
            }
            return userLogic.Save(user); 
        }

        internal User Edit(UserEditVM model)
        {
            User user = Mapper.Map<User>(model);
            if (model.Photo != null)
            {
                WebImage img = new WebImage(model.Photo.InputStream);
                img.Resize(imageWidth, imageHeight);
                user.Photo = new PictureData();
                user.Photo.Data = img.GetBytes();
                user.Photo.ContentType = "image/" + img.ImageFormat;
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

        public List<Award> GetAwards(int id)
        {
            var user = userLogic.GetUserByID(id);
            if(user != null)
            {
                return userLogic.GetAwards(user);
            }
            else
            {
                return null;
            }
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