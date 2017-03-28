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
using System.Configuration;

namespace Epam.UsersAwards.MVC.Models
{
    public class AwardDM
    {
        private IAwardLogic awardLogic;
        public int imageHeight;
        public int imageWidth;
        public int thumbnailHeight;
        public int thumbnailWidth;
        public AwardDM(IAwardLogic awardLogic)
        {
            this.awardLogic = awardLogic;
            imageHeight = Convert.ToInt32(ConfigurationManager.AppSettings.Get("imageHeight"));
            imageWidth = Convert.ToInt32(ConfigurationManager.AppSettings.Get("imageWidth"));
            thumbnailHeight = Convert.ToInt32(ConfigurationManager.AppSettings.Get("thumbnailHeight"));
            thumbnailWidth = Convert.ToInt32(ConfigurationManager.AppSettings.Get("thumbnailWidth"));
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

        internal AwardEditVM GetAwardByID(int id)
        {
            var award = awardLogic.GetAwardByID(id);
            return Mapper.Map<AwardEditVM>(award);
        }

        internal Award Save(AwardCreateVM model)
        {
            var award = Mapper.Map<Award>(model);
            if (model.Image != null)
            {
                WebImage img = new WebImage(model.Image.InputStream);
                img.Resize(imageWidth, imageHeight);
                award.Image = new PictureData();
                award.Image.Data = img.GetBytes();
                award.Image.ContentType = "image/" + img.ImageFormat;
            }
            else// for web api
            {
                var image = new WebImage(@"D:\it2017_1\Task1\Epam.UsersAwards.MVC\Content\Images\award-default.png");
                award.Image.ContentType = "image/" + image.ImageFormat;
                award.Image.Data = image.GetBytes();
            }
            return awardLogic.Save(award);
        }


        internal Award Edit(AwardEditVM model)
        {
            var award = Mapper.Map<Award>(model);
            if (model.Image != null)
            {
                WebImage img = new WebImage(model.Image.InputStream);
                img.Resize(imageWidth, imageHeight);
                award.Image = new PictureData();
                award.Image.Data = img.GetBytes();
                award.Image.ContentType = "image/" + img.ImageFormat;
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
            pic.Data = img.Resize(thumbnailWidth, thumbnailHeight).GetBytes();
            return pic;
        }

    }
}