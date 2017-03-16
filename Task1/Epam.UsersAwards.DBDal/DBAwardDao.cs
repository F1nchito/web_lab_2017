using Epam.UsersAwards.DalContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.UsersAwards.Entities;

namespace Epam.UsersAwards.DBDal
{
    public class DBAwardDao : IAwardDao
    {
        public Award Add(Award award)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int awardID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Award> GetAllAwards()
        {
            throw new NotImplementedException();
        }

        public Award GetAwardByID(int id)
        {
            throw new NotImplementedException();
        }

        public Award Update(int awardID, string title)
        {
            throw new NotImplementedException();
        }
    }
}
