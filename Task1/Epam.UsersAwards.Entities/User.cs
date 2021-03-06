﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.UsersAwards.Entities
{
    public class User
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime DOB { get; set; }

        public int Age
        {
            get
            {
                DateTime now = DateTime.Now;
                int years = now.Year - DOB.Year;
                if (now.Month < DOB.Month)
                {
                    years--;
                }else if(now.Month == DOB.Month && now.Day < DOB.Day)
                {
                    years--;
                }
                return years;
            }
        }

        public PictureData Photo { get; set; }

        public List<Award> Awards { get; set; }
    }
}
