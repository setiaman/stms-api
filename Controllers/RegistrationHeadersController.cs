using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using stms_api.Models;

namespace stms_api.Controllers
{
    public class RegistrationHeadersController : ODataController
    {
        private stmsContext db = new stmsContext();

        private bool RegistrationHeaderExists(int key)
        {
            return db.RegistrationHeaders.Count(e => e.ID == key) > 0;
        }

        // GET: odata/RegistrationHeaders
        [EnableQuery]
        public IQueryable<RegistrationHeader> GetRegistrationHeaders()
        {
            return db.RegistrationHeaders;
        }

        // GET: odata/RegistrationHeaders(5)
        [EnableQuery]
        public SingleResult<RegistrationHeader> GetRegistrationHeaders([FromODataUri] int key)
        {
            return SingleResult.Create(db.RegistrationHeaders.Where(@RegistrationHeader => @RegistrationHeader.ID == key));
        }

        // POST: odata/RegistrationHeader
        public async Task<IHttpActionResult> Post(RegistrationHeader registrationHeader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RegistrationHeaders.Add(registrationHeader);
            await db.SaveChangesAsync();

            return Created(registrationHeader);
        }

        // PATCH: odata/RegistrationHeaders(5) 
        // this method used for update 
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<RegistrationHeader> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RegistrationHeader registrationHeader = await db.RegistrationHeaders.FindAsync(key);
            if (registrationHeader == null)
            {
                return NotFound();
            }

            patch.Patch(registrationHeader);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistrationHeaderExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(registrationHeader);
        }

    }
}

