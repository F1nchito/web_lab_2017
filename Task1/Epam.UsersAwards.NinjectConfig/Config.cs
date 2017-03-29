using Epam.UsersAwards.DalContracts;
using Epam.UsersAwards.LogicContracts;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.UsersAwards.NinjectConfig
{
    public static class Config
    {
        public static IKernel RegisterServices(IKernel kernel)
        {
            kernel
                .Bind<IUserLogic>()
                .To<Epam.UsersAwards.Logic.UserLogic>()
                .InSingletonScope();
            kernel
                .Bind<IAwardLogic>()
                .To<Epam.UsersAwards.Logic.AwardLogic>()
                .InSingletonScope();
            kernel
                .Bind<IUserDao>()
                .To<UsersAwards.DBDal.DBUserDao>();
            kernel
                .Bind<IAwardDao>()
                .To<UsersAwards.DBDal.DBAwardDao>();
            return kernel;
        }
    }
}
