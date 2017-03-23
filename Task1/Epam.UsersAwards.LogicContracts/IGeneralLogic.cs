using Epam.UsersAwards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.UsersAwards.LogicContracts
{
    public interface IGeneralLogic<T>
    {
        List<T> GetAll();
        T Save(T entitiy);
        T Update(T entitiy);
        bool Delete(int entitiyID);
        PictureData GetPicture(int id);

    }
}
