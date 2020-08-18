using PhoneBookAppCF.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PhoneBookAppCF.Models;

namespace PhoneBookWebService.Controllers
{
    public class CountriesController : ApiController
    {
        private PersonContext db = new PersonContext();
        public IEnumerable<Country>Get()
        {
            var countries = db.Countries.Where(c => c.IsActive).ToList();
            return countries;
        }
    }
}
