using Epam.UsersAwards.DalContracts;
using Epam.UsersAwards.LogicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.UsersAwards.Entities;

namespace Epam.UsersAwards.Logic
{
    public class AwardLogic : IAwardLogic
    {
        public IAwardDao awardDao;
        public AwardLogic()
        {
            awardDao = DaoProvider.AwardDao;
        }

        public bool Delete(int ID)
        {
            throw new NotImplementedException();
        }

        public Award[] GetAll()
        {
            throw new NotImplementedException();
        }

        public Award GetAwardByID(int awardID)
        {
            throw new NotImplementedException();
        }

        public Award Save(string AwardName, string Description)
        {
            throw new NotImplementedException();
        }

        public Award Update(int ID, string Title)
        {
            throw new NotImplementedException();
        }
    }
}
