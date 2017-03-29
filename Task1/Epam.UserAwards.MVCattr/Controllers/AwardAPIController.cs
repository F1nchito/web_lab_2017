using Epam.UsersAwards.Entities;
using Epam.UsersAwards.MVCattr.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Epam.UsersAwards.MVCattr.Models;
using Epam.UsersAwards.MVCattr.ViewModels.Awards;

namespace Epam.UsersAwards.MVCattr.Controllers
{
    [RoutePrefix("api/award")]
    public class AwardAPIController : ApiController
    {
        private AwardDM awardDm;
        public AwardAPIController(AwardDM awardDm)
        {
            this.awardDm = awardDm;
        }

        [Route("")]
        public IHttpActionResult Get(string filter=null)
        {
            if (string.IsNullOrEmpty(filter))
            {
                var awards = awardDm.GetAll();
                return Json(awards);
            }
            else
            {
                var awards = awardDm.GetAwardsByFilter(filter);
                return Json(awards);
            }
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID should be positive");
            }
            var award = awardDm.GetAwardByID(id);
            if (award == null)
            {
                return NotFound();
            }
            return Json(award);
        }

        //public async Task<IHttpActionResult> Upload()
        //{
        //    if (!Request.Content.IsMimeMultipartContent())
        //        throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

        //    var provider = new MultipartMemoryStreamProvider();
        //    await Request.Content.ReadAsMultipartAsync(provider);
        //    foreach (var file in provider.Contents)
        //    {
        //        var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
        //        var buffer = await file.ReadAsByteArrayAsync();
        //        //Do whatever you want with filename and its binaray data.
        //    }

        //    return Ok();
        //}

        [Route("")]
        public IHttpActionResult Post([FromBody]AwardCreateVM award)
        {
            if (award == null)
            {
                return BadRequest("Award should be defined in request body");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Award not valid");
            }
            else
            {
                var awardWithID = awardDm.Save(award);
                if (awardWithID != null)
                {
                    return Created($"api/user/{awardWithID.ID}", awardWithID);
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        // PUT: api/AwardAPI/5
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody]AwardEditVM award)
        {
            if (ModelState.IsValid)
            {
                if (awardDm.GetAwardByID(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    awardDm.Edit(award);
                    return Ok();
                }
            }
            else
            {
                return BadRequest("Award not valid");
            }
        }

        // DELETE: api/AwardAPI/5
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            if (awardDm.GetAwardByID(id) == null)
            {
                return NotFound();
            }
            else
            {
                awardDm.Delete(id);
                return Ok();
            }
        }
    }
}
