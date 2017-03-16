using Epam.UsersAwards.DalContracts;
using Epam.UsersAwards.DBDal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.UsersAwards.Logic
{
    public class DaoProvider
    {
        static DaoProvider()
        {
            switch (ConfigurationManager.AppSettings["db"])
            {
                //case "file":
                //    AwardDao = new FileAwardDao();
                //    UserDao = new FileUserDao();
                //    SecurityDao = new FileSecurityDao();
                //    break;
                case "sql":
                    AwardDao = new DBAwardDao();
                    UserDao = new DBUserDao();
                    //SecurityDao = new DBSecurityDao();
                    //AvatarDao = new DBAvatarDao();
                    break;
                default:
                    throw new InvalidCastException();
            }
        }
        public static IUserDao UserDao { get; }
        public static IAwardDao AwardDao { get; }
        //public static ISecurityDao SecurityDao { get; }
        //public static IAvatarDao AvatarDao { get; }
    }
}
