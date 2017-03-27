using AutoMapper;
using Epam.UsersAwards.Entities;
using Epam.UsersAwards.Logic;
using Epam.UsersAwards.LogicContracts;
using Epam.UsersAwards.MVC.Models;
using Epam.UsersAwards.MVC.ViewModels;
using Epam.UsersAwards.MVC.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Epam.UsersAwards.MVC.Controllers
{
    public class UserController : Controller
    {
        private UserDM userDm;

        public UserController(UserDM userDm)
        {
            this.userDm = userDm;
        }
        // GET: Users
        public ActionResult Index()
        {
            var users = userDm.GetAll();
            return View(users);
        }

        public ActionResult GetByName(string name)
        {
            var model = userDm.GetUserByName(name);
            return View(model);
        }

        public ActionResult GetByFilter(string filter)
        {
            var model = userDm.GetUsersByFilter(filter);
            return View(model);
        }

        public ActionResult GetByID(int id)
        {
            var model = userDm.GetUserByID(id);
            return View(model);
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserCreateVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    userDm.Save(model);
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

        [HttpGet]
        public ActionResult Photo(int id)
        {
            PictureData photo = userDm.GetPicture(id);
            if (photo == null)
            {
                //TODO: Перенсти в логику?
                return File(@"\Content\Images\anonymous-user.png", "image/png");
            }
            return File(photo.Data, photo.ContentType);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            //TODO: State is valid
            var model = userDm.GetUserByID(id);
            return View(model);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserEditVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    userDm.Edit(model);
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

        // GET: Users/Delete/5
        public ActionResult Delete()
        {
            return View();
        }

        // POST: Users/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                userDm.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult AddAward(int userID, int awardID)
        {
            try
            {
                userDm.AddAwardToUser(userID, awardID);
                return RedirectToRoute("Alls");
            }
            catch 
            {
                return View();
            }
        }

        public FileResult GetAllAsFile()
        {
            byte[] usersBytes = userDm.GetAllAsFile();
            return File(usersBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "Users.txt");
        }
    }
}
