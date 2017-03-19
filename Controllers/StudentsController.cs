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
    builder.EntitySet<Student>("Students");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class StudentsController : ODataController
    {
        private stmsContext db = new stmsContext();

        // GET: odata/Students
        [EnableQuery]
        public IQueryable<Student> GetStudents()
        {
            return db.Students;
        }

        // GET: odata/Students(5)
        [EnableQuery]
        public SingleResult<Student> GetStudent([FromODataUri] int key)
        {
            return SingleResult.Create(db.Students.Where(student => student.ID == key));
        }

        // PUT: odata/Students(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Student> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Student student = await db.Students.FindAsync(key);
            if (student == null)
            {
                return NotFound();
            }

            patch.Put(student);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(student);
        }

        // POST: odata/Students
        public async Task<IHttpActionResult> Post(Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Students.Add(student);
            await db.SaveChangesAsync();

            return Created(student);
        }

        // PATCH: odata/Students(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Student> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Student student = await db.Students.FindAsync(key);
            if (student == null)
            {
                return NotFound();
            }

            patch.Patch(student);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(student);
        }

        // DELETE: odata/Students(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Student student = await db.Students.FindAsync(key);
            if (student == null)
            {
                return NotFound();
            }

            db.Students.Remove(student);
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

        private bool StudentExists(int key)
        {
            return db.Students.Count(e => e.ID == key) > 0;
        }
    }
}
