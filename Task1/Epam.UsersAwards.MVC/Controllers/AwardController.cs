using Epam.UsersAwards.Entities;
using Epam.UsersAwards.MVC.Models;
using Epam.UsersAwards.MVC.ViewModels.Awards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epam.UsersAwards.MVC.Controllers
{
    public class AwardController : Controller
    {
        private AwardDM awardDm;
        public AwardController(AwardDM awardDm)
        {
            this.awardDm = awardDm;
        }
        // GET: Awards
        public ActionResult Index()
        {
            var awards = awardDm.GetAll();
            return View(awards);
        }

        public ActionResult GetByName(string name)
        {
            var model = awardDm.GetAwardByName(name);
            return View(model);
        }

        public ActionResult GetByFilter(string filter)
        {
            var model = awardDm.GetAwardsByFilter(filter);
            return View(model);
        }

        public ActionResult GetByID(int id)
        {
            var model = awardDm.GetAwardForEdit(id);
            return View(model);
        }
        // GET: Awards/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Awards/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Awards/Create
        [HttpPost]
        public ActionResult Create(AwardCreateVM model)
        {
            try
            {
                awardDm.Save(model);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Awards/Edit/5
        public ActionResult Edit(int id)
        {
            AwardEditVM model = awardDm.GetAwardForEdit(id);
            return View(model);
        }

        // POST: Awards/Edit/5
        [HttpPost]
        public ActionResult Edit(AwardEditVM model)
        {
            try
            {
                awardDm.Edit(model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Awards/Delete/5
        public ActionResult Delete()
        {
            return View();
        }

        // POST: Awards/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                awardDm.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Photo(int id)
        {
            PictureData photo = awardDm.GetPicture(id);
            if (photo == null)
            {
                return File(@"\Content\Images\anonymous-user.png", "image/png");
            }
            return File(photo.Data, photo.ContentType);
        }
        [HttpGet]
        public ActionResult SmallPhoto(int id)
        {
            PictureData photo = awardDm.GetThumbnail(id);
            if (photo == null)
            {
                return File(@"\Content\Images\anonymous-user.png", "image/png");
            }
            return File(photo.Data, photo.ContentType);
        }
    }
}
