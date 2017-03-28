using Epam.UsersAwards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Epam.UsersAwards.MVCattr.Models;
using Epam.UsersAwards.MVCattr.ViewModels;
using Epam.UsersAwards.MVCattr.ViewModels.Users;

namespace Epam.UsersAwards.MVCattr.Controllers
{
    [RoutePrefix("api/user")]
    public class UserApiController : ApiController
    {
        private UserDM userDm;
        public UserApiController(UserDM userDm)
        {
            this.userDm = userDm;
        }

        // GET: api/UserApi
        [Route("")]
        public IHttpActionResult Get(string filter = null)
        {
            if (string.IsNullOrEmpty(filter))
            {
                var users = userDm.GetAll();
                return Json(users);
            }
            else
            {
                var users = userDm.GetUsersByFilter(filter);
                return Json(users);
            }

        }

       
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID should be positive");
            }
            var user = userDm.GetUserByID(id);
            if(user == null)
            {
                return NotFound();
            }
            return Json(user);
        }

        [Route("{id:int}/awards")]
        public IHttpActionResult GetAwards(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID should be positive");
            }
            if (userDm.GetUserByID(id) == null)
            {
                return NotFound();
            }
            var awards = userDm.GetAwards(id);
            return Json(awards);
        }

        [Route("")]
        public IHttpActionResult Post([FromBody]UserCreateVM user)
        {
            if (user == null)
            {
                return BadRequest("User should be defined in request body");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("User not valid");
            }
            else
            {
                var userWithID = userDm.Save(user);
                if (userWithID != null)
                {
                    return Created($"api/user/{userWithID.ID}", userWithID);
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        // PUT: api/UserApi/5
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody]UserEditVM user)
        {
            if (ModelState.IsValid)
            {
                if (userDm.GetUserByID(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    userDm.Edit(user);
                    return Ok();
                }
            }
            else
            {
                return BadRequest("User not valid");
            }
        }
        [Route("{id:int}/awards/{awardID:int}")]
        public IHttpActionResult Put(int id, int awardID)
        {
            if (userDm.AddAwardToUser(id, awardID))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/UserApi/5
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            if (userDm.GetUserByID(id) == null)
            {
                return NotFound();
            }
            else
            {
                userDm.Delete(id);
                return Ok();
            }
        }
    }
}
