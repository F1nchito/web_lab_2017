using Epam.UsersAwards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epam.UsersAwards.MVCattr.ViewModels.Users
{
    public class UserShowWithAwardsVM
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime DOB { get; set; }

        public int Age { get; set; }

        public List<Award> Awards { get; set; }
    }
}