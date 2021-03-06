﻿using System;
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
    builder.EntitySet<Trainer>("Trainers");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class TrainersController : ODataController
    {
        private stmsContext db = new stmsContext();

        // GET: odata/Trainers
        [EnableQuery]
        public IQueryable<Trainer> GetTrainers()
        {
            return db.Trainers;
        }

        // GET: odata/Trainers(5)
        [EnableQuery]
        public SingleResult<Trainer> GetTrainer([FromODataUri] int key)
        {
            return SingleResult.Create(db.Trainers.Where(trainer => trainer.ID == key));
        }

        // PUT: odata/Trainers(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Trainer> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Trainer trainer = await db.Trainers.FindAsync(key);
            if (trainer == null)
            {
                return NotFound();
            }

            patch.Put(trainer);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainerExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(trainer);
        }

        // POST: odata/Trainers
        public async Task<IHttpActionResult> Post(Trainer trainer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Trainers.Add(trainer);
            await db.SaveChangesAsync();

            return Created(trainer);
        }

        // PATCH: odata/Trainers(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Trainer> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Trainer trainer = await db.Trainers.FindAsync(key);
            if (trainer == null)
            {
                return NotFound();
            }

            patch.Patch(trainer);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainerExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(trainer);
        }

        // DELETE: odata/Trainers(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Trainer trainer = await db.Trainers.FindAsync(key);
            if (trainer == null)
            {
                return NotFound();
            }

            db.Trainers.Remove(trainer);
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

        private bool TrainerExists(int key)
        {
            return db.Trainers.Count(e => e.ID == key) > 0;
        }
    }
}
