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
        [ValidateAntiForgeryToken]
        public ActionResult Create(AwardCreateVM model)
        {
            ViewBag.Breadcrumb = new Breadcrumb("award", "create", null);
            try
            {
                if (ModelState.IsValid)
                {
                var award = awardDm.Save(model);
                    if(award == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Awards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(400);
            }
            AwardEditVM model = awardDm.GetAwardByID((int)id);
            if(model == null)
            {
                return HttpNotFound();
            }
            ViewBag.Breadcrumb = new Breadcrumb("award", "edit", model.Title);
            return View(model);
        }

        // POST: Awards/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AwardEditVM model)
        {
            ViewBag.Breadcrumb = new Breadcrumb("award", "edit", model.Title);
            try
            {
                if (ModelState.IsValid)
                {
                    var award = awardDm.Edit(model);
                    if (award == null)
                    {
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Awards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(400);
            }
            var award = awardDm.GetAwardByID((int)id);
            if (award == null)
            {
                return HttpNotFound();
            }
            ViewBag.Breadcrumb = new Breadcrumb("award", "delete", award.Title);
            return View(award);
        }

        // POST: Awards/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Award award)
        {
            try
            {
                ViewBag.Breadcrumb = new Breadcrumb("award", "delete", award.Title);
                var result = awardDm.Delete(award.ID);
                if (result)
                {
                return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
        public ActionResult AwardPartial(int id)
        {
            var award = awardDm.GetAwardByID(id);
            if (award != null)
            {
                return PartialView("_AwardPartialModal", award);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpGet]
        public ActionResult Photo(int id)
        {
            PictureData photo = awardDm.GetPicture(id);
            if (photo == null)
            {
                return File(@"\Content\Images\award-default.png", "image/png");
            }
            return File(photo.Data, photo.ContentType);
        }
        [HttpGet]
        public ActionResult SmallPhoto(int id)
        {
            PictureData photo = awardDm.GetThumbnail(id);
            if (photo == null)
            {
                return File(@"\Content\Images\award-default-small.png", "image/png");
            }
            return File(photo.Data, photo.ContentType);
        }
    }
}
