using Epam.UsersAwards.LogicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Epam.UsersAwards.MVC.ViewModels.Awards;
using Epam.UsersAwards.Entities;
using AutoMapper;
using System.IO;
using System.Web.Helpers;

namespace Epam.UsersAwards.MVC.Models
{
    public class AwardDM
    {
        private IAwardLogic awardLogic;
        public AwardDM(IAwardLogic awardLogic)
        {
            this.awardLogic = awardLogic;
        }

        internal List<Award> GetAll()
        {
            return awardLogic.GetAll();
        }

        public Award GetAwardByName(string name)
        {
            var replacedName = name.Replace('_', ' ');
            return awardLogic.GetAwardByName(replacedName);
        }

        public List<Award> GetAwardsByFilter(string filter)
        {
            return awardLogic.GetAwardsByFilter(filter);
        }

        internal AwardEditVM GetAwardForEdit(int id)
        {
            var award = awardLogic.GetAwardByID(id);
            return Mapper.Map<AwardEditVM>(award);
        }

        internal bool Save(AwardCreateVM model)
        {
            var award = Mapper.Map<Award>(model);
            if (model.Image != null)
            {
                award.Image = new PictureData();
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    model.Image.InputStream.CopyTo(memoryStream);
                    award.Image.Data = memoryStream.ToArray();
                    award.Image.ContentType = model.Image.ContentType;
                }
            }
            return (awardLogic.Save(award) != null);
        }


        internal Award Edit(AwardEditVM model)
        {
            var award = Mapper.Map<Award>(model);
            if (model.Image != null)
            {
                award.Image = new PictureData();
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    model.Image.InputStream.CopyTo(memoryStream);
                    award.Image.Data = memoryStream.ToArray();
                    award.Image.ContentType = model.Image.ContentType;
                }
            }
            return awardLogic.Update(award);
        }

        internal bool Delete(int id)
        {
            return awardLogic.Delete(id);
        }

        internal PictureData GetPicture(int id)
        {
            return awardLogic.GetPicture(id);
        }
        internal PictureData GetThumbnail(int id)
        {
            var pic = awardLogic.GetPicture(id);
            var img = new WebImage(pic.Data);
            pic.Data = img.Resize(50, 50).GetBytes();
            return pic;
        }

    }
}