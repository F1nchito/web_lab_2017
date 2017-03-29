using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epam.UsersAwards.MVC.ViewModels.Users
{
    public class UserShowVM
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime DOB { get; set; }

        public int Age { get; set; }
    }
}