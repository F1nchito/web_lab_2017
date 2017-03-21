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
        Award Save(Award award);
        List<Award> GetAll();
        Award Update(Award award);
        bool Delete(int ID);
        Award GetAwardByID(int awardID);
        PictureData GetPicture(int id);
    }
}
