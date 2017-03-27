using Epam.UsersAwards.Entities;
using Epam.UsersAwards.MVC.Models;
using Epam.UsersAwards.MVC.ViewModels;
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
            ViewBag.Breadcrumb = new Breadcrumb("awards", null, null);
            return View(awards);
        }

        public ActionResult GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new HttpStatusCodeResult(400);
            }
            var model = awardDm.GetAwardByName(name);
            ViewBag.Breadcrumb = new Breadcrumb("awards", "search", null);
            return View(model);
        }

        public ActionResult GetByFilter(string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return new HttpStatusCodeResult(400);
            }
            var model = awardDm.GetAwardsByFilter(filter);
            ViewBag.Breadcrumb = new Breadcrumb("awards", "search", null);
            return View(model);
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(nameof(Index));
            }
            var award = awardDm.GetAwardByID((int)id);
            if (award == null)
            {
                return HttpNotFound();
            }
            ViewBag.Breadcrumb = new Breadcrumb("award", null, award.Title);
            return View(award);
        }

        // GET: Awards/Create
        public ActionResult Create()
        {
            ViewBag.Breadcrumb = new Breadcrumb("award", "create", null);
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
            AwardEditVM model = awardDm.GetAwardByID(id);
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
                return File(@"\Content\Images\award-clipart-award-clipart-172_293.jpg", "image/jpeg");
            }
            return File(photo.Data, photo.ContentType);
        }
        [HttpGet]
        public ActionResult SmallPhoto(int id)
        {
            PictureData photo = awardDm.GetThumbnail(id);
            if (photo == null)
            {
                return File(@"\Content\Images\award-clipart-award-clipart-172_293.jpg", "image /jpeg");
            }
            return File(photo.Data, photo.ContentType);
        }
    }
}
