using Epam.UsersAwards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.UsersAwards.DalContracts
{
    public interface IGeneralDao<T>
    {
        T Add(T user);
        bool Delete(int entitiyID);
        T Update(T award);
        PictureData GetPicture(int id);

    }
}
