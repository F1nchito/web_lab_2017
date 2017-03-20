﻿using Epam.UsersAwards.LogicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Epam.UsersAwards.MVC.ViewModels.Awards;
using Epam.UsersAwards.Entities;
using AutoMapper;

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

        internal bool Save(AwardCreateVM model)
        {
            var award = Mapper.Map<Award>(model);
            return (awardLogic.Save(award) != null);
        }

        internal Award Edit(AwardEditVM model)
        {
            var award = Mapper.Map<Award>(model);
            return awardLogic.Update(award);
        }

        internal bool Delete(int id)
        {
            return awardLogic.Delete(id);
        }

        internal AwardEditVM GetAwardForEdit(int id)
        {
            var award = awardLogic.GetAwardByID(id);
            return Mapper.Map<AwardEditVM>(award);
        }
    }
}