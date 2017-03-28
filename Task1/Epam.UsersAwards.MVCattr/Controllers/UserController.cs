using AutoMapper;
using Epam.UsersAwards.Entities;
using Epam.UsersAwards.Logic;
using Epam.UsersAwards.LogicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Epam.UsersAwards.MVCattr.Models;
using Epam.UsersAwards.MVCattr.ViewModels;
using Epam.UsersAwards.MVCattr.ViewModels.Users;

namespace Epam.UsersAwards.MVCattr.Controllers
{
    [RoutePrefix("user")]
    public class UserController : Controller
    {
        private UserDM userDm;

        public UserController(UserDM userDm)
        {
            this.userDm = userDm;
        }
        [HttpGet]
        public ActionResult Photo(int id)
        {
            PictureData photo = userDm.GetPicture(id);
            if (photo == null)
            {
                return File(@"\Content\Images\user-default.png", "image/png");
            }
            return File(photo.Data, photo.ContentType);
        }
        // GET: Users
        [Route("~/users")]
        public ActionResult Index()
        {
            var model = userDm.GetAll();
            ViewBag.Breadcrumb = new Breadcrumb("users", null, null);
            return View(model);
        }
        [Route("{name}")]
        public ActionResult GetByName(string name)
        {
            ViewBag.Breadcrumb = new Breadcrumb("users", "search", null);
            if (string.IsNullOrEmpty(name))
            {
                return new HttpStatusCodeResult(400);
            }
            var model = userDm.GetUserByName(name);
            return View(model);
        }

        [Route("~/users/{filter:length(1,50)}")]
        public ActionResult GetByFilter(string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return new HttpStatusCodeResult(400);
            }
            var model = userDm.GetUsersByFilter(filter);
            ViewBag.Breadcrumb = new Breadcrumb("users", "search", null);
            return View(model);
        }

        [Route("{id:int?}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var user = userDm.GetUserByID((int)id);
            if (user == null)
            {
                return HttpNotFound();
            }
            else
            {
                var model = Mapper.Map<UserShowWithAwardsVM>(user);
                ViewBag.Breadcrumb = new Breadcrumb("user", null, user.Name);
                return View(model);
            }
        }

        // GET: Users/Create
        [Route("~/create-user")]
        public ActionResult Create()
        {
            ViewBag.Breadcrumb = new Breadcrumb("user", "create", null);
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("~/create-user")]
        public ActionResult Create(UserCreateVM model)
        {
            ViewBag.Breadcrumb = new Breadcrumb("user", "create", null);
            try
            {
                if (ModelState.IsValid)
                {
                    var user = userDm.Save(model);
                    if(user == null)
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

        

        // GET: Users/Edit/5
        [Route("{id:int}/edit")]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }
            var user = userDm.GetUserByID((int)id);
            if (user == null)
            {
                return HttpNotFound();
            }
            else
            {
                var model = Mapper.Map<UserEditVM>(user);
                ViewBag.Breadcrumb = new Breadcrumb("user", "edit", user.Name);
                return View(model);
            }
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{id:int}/edit")]
        public ActionResult Edit(UserEditVM model)
        {
            ViewBag.Breadcrumb = new Breadcrumb("user", "edit", model.Name);
            if (ModelState.IsValid)
                {
                    userDm.Edit(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    //TODO: ERROR EVERYWHERE!
                    ModelState.AddModelError("","qwe");
                    return View();
                }
        }

        // GET: Users/Delete/5
        [Route("{id:int}/delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(400);
            }
            var user = userDm.GetUserByID((int)id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Breadcrumb = new Breadcrumb("user", "delete", user.Name);
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{id:int}/delete")]
        public ActionResult Delete(UserShowWithAwardsVM user)
        {
            ViewBag.Breadcrumb = new Breadcrumb("user", "delete", user.Name);
            if (userDm.Delete(user.ID))
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return new HttpStatusCodeResult(400);
            }
        }

        [HttpPost]
        [Route("~/award-user/{userID:int}_{awardID:int?}")]
        public ActionResult AddAward(int userID, int awardID)
        {
            try
            {
                userDm.AddAwardToUser(userID, awardID);
                return RedirectToAction(nameof(Index));
            }
            catch 
            {
                return View();
            }
        }

        [Route("~/users/file")]
        public FileResult GetAllAsFile()
        {
            byte[] usersBytes = userDm.GetAllAsFile();
            return File(usersBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "Users.txt");
        }
    }
}
