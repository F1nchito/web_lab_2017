using Epam.UsersAwards.Entities;
using Epam.UsersAwards.MVC.Models;
using Epam.UsersAwards.MVC.ViewModels;
using Epam.UsersAwards.MVC.ViewModels.Awards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Epam.UsersAwards.MVC.Controllers
{
    [RoutePrefix("api/award")]
    public class AwardAPIController : ApiController
    {
        private AwardDM awardDm;
        public AwardAPIController(AwardDM awardDm)
        {
            this.awardDm = awardDm;
        }
        // GET: api/AwardAPI
        [Route("")]
        public List<Award> Get(string filter=null)
        {
            if (string.IsNullOrEmpty(filter))
            {
                var awards = awardDm.GetAll();
                return awards;
            }
            else
            {
                var awards = awardDm.GetAwardsByFilter(filter);
                return awards;
            }
        }

        // GET: api/AwardAPI/5
        [Route("{id:int}")]
        public AwardEditVM Get(int id)
        {
            var award = awardDm.GetAwardByID(id);
            return award;
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
        // POST: api/AwardAPI
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
                return Created($"api/user/{awardWithID.ID}", awardWithID);
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
