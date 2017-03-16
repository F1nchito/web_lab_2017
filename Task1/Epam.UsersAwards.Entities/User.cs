using System;
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
        private DateTime dob;
        private string userName;
        private DateTime userDOB;

        public User(string userName, DateTime userDOB)
        {
            this.userName = userName;
            this.userDOB = userDOB;
        }

        public DateTime DOB
        {
            get { return dob; }
            set
            {
                if (DateTime.Now < this.DOB || DOB.AddYears(150)<= DateTime.Now)
                {
                    throw new ArgumentException("Недопустимая дата рождения");
                }
                dob = value;
            }
        }
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
    }
}
