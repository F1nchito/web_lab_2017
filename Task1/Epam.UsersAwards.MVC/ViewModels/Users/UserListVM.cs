﻿using Epam.UsersAwards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epam.UsersAwards.MVC.ViewModels.Users
{
    public class UserListVM
    {
        public IEnumerable<User> Users { get; set; }
    }
}