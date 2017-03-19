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
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<KeyPool>("KeyPools");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class KeyPoolsController : ODataController
    {
        private stmsContext db = new stmsContext();

        // GET: odata/KeyPools
        [EnableQuery]
        public IQueryable<KeyPool> GetKeyPools()
        {
            return db.KeyPools;
        }

        // GET: odata/KeyPools(5)
        [EnableQuery]
        public SingleResult<KeyPool> GetKeyPool([FromODataUri] string key)
        {
            return SingleResult.Create(db.KeyPools.Where(keyPool => keyPool.Code == key));
        }

        // PUT: odata/KeyPools(5)
        public async Task<IHttpActionResult> Put([FromODataUri] string key, Delta<KeyPool> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            KeyPool keyPool = await db.KeyPools.FindAsync(key);
            if (keyPool == null)
            {
                return NotFound();
            }

            patch.Put(keyPool);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KeyPoolExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(keyPool);
        }

        // POST: odata/KeyPools
        public async Task<IHttpActionResult> Post(KeyPool keyPool)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.KeyPools.Add(keyPool);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (KeyPoolExists(keyPool.Code))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(keyPool);
        }

        // PATCH: odata/KeyPools(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] string key, Delta<KeyPool> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            KeyPool keyPool = await db.KeyPools.FindAsync(key);
            if (keyPool == null)
            {
                return NotFound();
            }

            patch.Patch(keyPool);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KeyPoolExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(keyPool);
        }

        // DELETE: odata/KeyPools(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] string key)
        {
            KeyPool keyPool = await db.KeyPools.FindAsync(key);
            if (keyPool == null)
            {
                return NotFound();
            }

            db.KeyPools.Remove(keyPool);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KeyPoolExists(string key)
        {
            return db.KeyPools.Count(e => e.Code == key) > 0;
        }
    }
}
