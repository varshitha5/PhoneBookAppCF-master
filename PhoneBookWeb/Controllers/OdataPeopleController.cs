using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using PhoneBook.DAL;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;

namespace PhoneBookWeb.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

   
    using PhoneBook.DAL;
  
    */
    public class OdataPeopleController : ODataController
    {
        private PersonContext db = new PersonContext();

        // GET: odata/OdataPeople
        [EnableQuery]
        public IQueryable<Person> GetOdataPeople()
        {
            return db.Persons;
        }

        // GET: odata/OdataPeople(5)
        [EnableQuery]
        public SingleResult<Person> GetPerson([FromODataUri] int key)
        {
            return SingleResult.Create(db.Persons.Where(person => person.ID == key));
        }

        // PUT: odata/OdataPeople(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Person> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Person person = db.Persons.Find(key);
            if (person == null)
            {
                return NotFound();
            }

            patch.Put(person);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(person);
        }

        // POST: odata/OdataPeople
        public IHttpActionResult Post(Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Persons.Add(person);
            db.SaveChanges();

            return Created(person);
        }

        // PATCH: odata/OdataPeople(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Person> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Person person = db.Persons.Find(key);
            if (person == null)
            {
                return NotFound();
            }

            patch.Patch(person);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(person);
        }

        // DELETE: odata/OdataPeople(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Person person = db.Persons.Find(key);
            if (person == null)
            {
                return NotFound();
            }

            db.Persons.Remove(person);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/OdataPeople(5)/City
        [EnableQuery]
        public SingleResult<City> GetCity([FromODataUri] int key)
        {
            return SingleResult.Create(db.Persons.Where(m => m.ID == key).Select(m => m.City));
        }

        // GET: odata/OdataPeople(5)/Country
        [EnableQuery]
        public SingleResult<Country> GetCountry([FromODataUri] int key)
        {
            return SingleResult.Create(db.Persons.Where(m => m.ID == key).Select(m => m.Country));
        }

        // GET: odata/OdataPeople(5)/State
        [EnableQuery]
        public SingleResult<State> GetState([FromODataUri] int key)
        {
            return SingleResult.Create(db.Persons.Where(m => m.ID == key).Select(m => m.State));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonExists(int key)
        {
            return db.Persons.Count(e => e.ID == key) > 0;
        }
    }
}
