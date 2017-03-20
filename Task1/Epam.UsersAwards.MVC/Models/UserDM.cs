using Epam.UsersAwards.LogicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Epam.UsersAwards.MVC.ViewModels;
using Epam.UsersAwards.MVC.ViewModels.Users;
using Epam.UsersAwards.Entities;
using AutoMapper;

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
            return Mapper.Map<UserEditVM>(user);
        }
        internal bool Save(UserCreateVM model)
        {
            var user = Mapper.Map<User>(model);
            return (userLogic.Save(user) != null);
        }

        internal User Edit(UserEditVM model)
        {
            User user = Mapper.Map<User>(model);
            return userLogic.Update(user);
        }

        internal bool Delete(int id)
        {
            return userLogic.userDelete(id);
        }
    }
}