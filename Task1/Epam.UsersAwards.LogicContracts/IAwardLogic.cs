using Epam.UsersAwards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.UsersAwards.LogicContracts
{
    public interface IAwardLogic : IGeneralLogic<Award>
    {
        Award GetAwardByID(int awardID);
        Award GetAwardByName(string name);
        List<Award> GetAwardsByFilter(string filter);
    }
}
