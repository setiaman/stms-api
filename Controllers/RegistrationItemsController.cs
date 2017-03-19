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
    public class RegistrationItemsController : ODataController
    {
        private stmsContext db = new stmsContext();

        private bool RegistrationItemExists(int key)
        {
            return db.RegistrationItems.Count(e => e.ID == key) > 0;
        }

        // GET: odata/RegistrationItems
        [EnableQuery]
        public IQueryable<RegistrationItem> GetRegistrationItems()
        {
            return db.RegistrationItems;
        }

        // GET: odata/RegistrationItems(5)
        [EnableQuery]
        public SingleResult<RegistrationItem> GetRegistrationItems([FromODataUri] int key)
        {
            return SingleResult.Create(db.RegistrationItems.Where(@RegistrationItem => @RegistrationItem.ID == key));
        }

        // POST: odata/RegistrationItems
        public async Task<IHttpActionResult> Post(RegistrationItem registrationItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RegistrationItems.Add(registrationItem);
            await db.SaveChangesAsync();

            return Created(registrationItem);
        }

        // PATCH: odata/RegistrationItems(5) 
        // this method used for update 
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<RegistrationItem> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RegistrationItem registrationItem = await db.RegistrationItems.FindAsync(key);
            if (registrationItem == null)
            {
                return NotFound();
            }

            patch.Patch(registrationItem);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistrationItemExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(registrationItem);
        }

    }
}

