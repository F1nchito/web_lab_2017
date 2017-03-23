using Epam.UsersAwards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.UsersAwards.DalContracts
{
    public interface IAwardDao : IGeneralDao<Award>
    {
        IEnumerable<Award> GetAllAwards();
        Award GetAwardByID(int id);
        Award GetAwardByName(string name);
        IEnumerable<Award> GetAwardsByFilter(string filter);
    }
}
