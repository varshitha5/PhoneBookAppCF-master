using PhoneBook.DAL;
using PhoneBook.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Web.Http.Cors;

namespace PhoneBookWeb.Controllers
{
    [EnableCors(origins: "http://mywebclient.azurewebsites.net", headers: "*", methods: "*")]
    public class CountriesController : ApiController
    {
        CountryService countryService;
        public CountriesController()
        {
            countryService = new CountryService();
        }
        public IEnumerable<Country> GetCountries()
        {
            return countryService.GetCountries();
        }
        public IEnumerable<Country> AddCountries(Country country)
        {
            return countryService.AddCountries(country);
        }

        [HttpPatch]
        public IHttpActionResult Patch(int id,[FromBody]Country country)
        {
            try
            {
                if (id == default(int))
                {
                    return NotFound();
                }
                country.CountryID = id;
                Country result = countryService.UpdateCountries(country);

                return Ok(result);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (id== default(int))
                {
                    return NotFound();
                }
                
                Country result = countryService.DeleteCountries(id);
                return Ok(result);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }



        
    }
}
