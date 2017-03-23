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
        private IAwardDao awardDao;
        public AwardLogic(IAwardDao awardDao)
        {
            this.awardDao = awardDao;
        }

        public bool Delete(int ID)
        {
            if(awardDao.GetAwardByID(ID) != null)
            {
                return awardDao.Delete(ID);
            }
            else
            {
                return false;
            }
        }

        public List<Award> GetAll()
        {
            return awardDao.GetAllAwards().ToList();
        }

        public Award GetAwardByID(int awardID)
        {
            return awardDao.GetAwardByID(awardID);
        }

        public Award GetAwardByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }
            else
            {
                return awardDao.GetAwardByName(name);
            }
        }

        public List<Award> GetAwardsByFilter(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                return null;
            }
            else
            {
                return awardDao.GetAwardsByFilter(filter).ToList();
            }
        }

        public PictureData GetPicture(int id)
        {
            return awardDao.GetPicture(id);
        }

        public Award Save(Award award)
        {
            if (string.IsNullOrWhiteSpace(award.Title))
            {
                return null;
            }
            if(awardDao.Add(award) != null)
            {
                return award;
            }
            throw new InvalidOperationException("Ошибка при сохранении");
        }

        public Award Update(Award award)
        {
            if (string.IsNullOrWhiteSpace(award.Title))
            {
                return null;
            }
            else if(awardDao.Update(award) != null)
            {
                return award;
            }
            throw new InvalidOperationException("Ошибка при сохранении");
        }
    }
}
