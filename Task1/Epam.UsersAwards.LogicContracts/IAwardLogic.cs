using Epam.UsersAwards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.UsersAwards.LogicContracts
{
    public interface IAwardLogic
    {
        Award Save(string AwardName, string Description);
        List<Award> GetAll();
        Award Update(int ID, string Title, string Description);
        bool Delete(int ID);
        Award GetAwardByID(int awardID);
    }
}
