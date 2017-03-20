using Epam.UsersAwards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.UsersAwards.DalContracts
{
    public interface IAwardDao
    {
        Award Add(Award award);
        IEnumerable<Award> GetAllAwards();
        Award GetAwardByID(int id);
        bool Delete(int awardID);
        Award Update(Award award);
    }
}
