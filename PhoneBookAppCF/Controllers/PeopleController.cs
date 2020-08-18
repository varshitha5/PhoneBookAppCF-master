using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhoneBook.DAL;

namespace PhoneBookAppCF.Controllers
{
    public class PeopleController : Controller
    {
        private PersonContext db = new PersonContext();

        // GET: People
        public ActionResult Index(String SearchString)
        {
            if (!String.IsNullOrEmpty(SearchString))
            {
                var people = db.Persons.Where(p => (p.FirstName.Contains(SearchString) ||
                                                   p.LastName.Contains(SearchString) ||
                                                   p.PhoneNumber.Contains(SearchString))) ;
                                                  
                return View(people.ToList());
            }
            var person = db.Persons.Include(p => p.City).Include(p => p.Country).Include(p => p.State);
            person = person.Where(p => p.IsActive.Equals(true));
            return View(person.ToList());
        }


         // GET: People/Create
        public ActionResult Create()
        {
            //ViewBag.CityID = new SelectList(db.City, "CityID", "CityName");
            //ViewBag.CountryID = new SelectList(db.Country, "CountryID", "CountryName");
            //ViewBag.StateID = new SelectList(db.State, "StateID", "StateName");
            //return View();
            List<Country> country = db.Countries.Where(c => c.IsActive).ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            

            foreach (var m in country)
            {


                li.Add(new SelectListItem { Text = m.CountryName, Value = m.CountryID.ToString() });
                ViewBag.country = li;

            }
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,PhoneNumber,Email,AddressLine1,AddressLine2,CityID,StateID,PinCode,CountryID,IsActive")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Persons.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.cityid = new SelectList(db.Cities, "cityid", "cityname", person.CityID);
            //ViewBag.countryid = new SelectList(db.Countries, "countryid", "countryname", person.CountryID);
            //ViewBag.stateiD = new SelectList(db.States, "StateID", "StateName", person.StateID);
            return View(person);
        }

        public JsonResult GetState(int id)
        {
            var states = db.States.Where(x => x.CountryID == id && x.IsActive).ToList();
            List<SelectListItem> listates = new List<SelectListItem>();

            listates.Add(new SelectListItem { Text = "--select state--", Value = "0" });
            if (states != null)
            {
                foreach (var x in states)
                {
                    listates.Add(new SelectListItem { Text = x.StateName, Value = x.StateID.ToString() });

                }



            }


            return Json(new SelectList(listates, "Value", "Text", JsonRequestBehavior.AllowGet));
        }


        public JsonResult getCity(int id)
        {
            var city = db.Cities.Where(c => c.StateID == id && c.IsActive).ToList();
            List<SelectListItem> licity = new List<SelectListItem>();

            licity.Add(new SelectListItem { Text = "--Select City--", Value = "0" });
            if (city != null)
            {
                foreach (var l in city)
                {
                    licity.Add(new SelectListItem { Text = l.CityName, Value = l.CityID.ToString() });

                }



            }


            return Json(new SelectList(licity, "Value", "Text", JsonRequestBehavior.AllowGet));
        }


  


// GET: People/Details/5
public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

       

        // GET: People/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }

            var country = db.Countries.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Select Country--", Value = "0" });

            foreach (var m in country)
            {


                li.Add(new SelectListItem { Text = m.CountryName, Value = m.CountryID.ToString() });
                ViewBag.country = li;

            }
            //return View();

            ViewBag.CityID = new SelectList(db.Cities, "CityID", "CityName", person.CityID);
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "CountryName", person.CountryID);
            ViewBag.StateID = new SelectList(db.States, "StateID", "StateName", person.StateID);
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,PhoneNumber,Email,AddressLine1,AddressLine2,CityID,StateID,PinCode,CountryID,IsActive")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "CityName", person.CityID);
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "CountryName", person.CountryID);
            ViewBag.StateID = new SelectList(db.States, "StateID", "StateName", person.StateID);
            return View(person);
        }

        // GET: People/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.Persons.Find(id);
            db.Persons.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
