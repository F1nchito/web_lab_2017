using Epam.UsersAwards.Entities;
using Epam.UsersAwards.MVC.Models;
using Epam.UsersAwards.MVC.ViewModels;
using Epam.UsersAwards.MVC.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Epam.UsersAwards.MVC.Controllers
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
        public List<User> Get(string filter = null)
        {
            if (string.IsNullOrEmpty(filter))
            {
                var users = userDm.GetAll();
                return users;
            }
            return null;

        }

        // GET: api/UserApi/5
        [Route("{id:int}")]
        public UserEditVM Get(int id)
        {
            var user = userDm.GetUserByID(id);
            return user;
        }

        // POST: api/UserApi
        [Route("")]
        public IHttpActionResult Post([FromBody]UserCreateVM user)
        {
            if(user == null)
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
                return Created($"api/user/{userWithID.ID}", userWithID);
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
