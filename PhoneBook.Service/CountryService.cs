using PhoneBook.DAL;
using System;
using System.Collections.Generic;
using System.Linq;


namespace PhoneBook.Service
{
    public class CountryService
    {
        PersonContext personContext;
        public CountryService()
        {
            personContext = new PersonContext();

        }
        public IEnumerable<Country> GetCountries()
        {
            return personContext.Countries.Where(c=>c.IsActive.Equals(true));
        }
        public IEnumerable<Country> AddCountries(Country country)
        {
            personContext.Countries.Add(country);
            personContext.SaveChanges();
            return personContext.Countries;
        }

        public Country UpdateCountries(Country country)
        {
            var existingCountry = personContext.Countries.FirstOrDefault(x => x.CountryID == country.CountryID);
            if (existingCountry == null)
            {
                throw new InvalidOperationException("Country Id NotFound");
            }
            existingCountry.CountryName = country.CountryName;
            personContext.SaveChanges();
            return country;
        }

        public Country DeleteCountries(int id)
        {
            var existingCountry = personContext.Countries.FirstOrDefault(x => x.CountryID == id && x.IsActive);
            if (existingCountry == null)
            {
                throw new InvalidOperationException("Country Id NotFound");
            }
            personContext.Countries.Remove(existingCountry);
            personContext.SaveChanges();
            return existingCountry;
        }



    }
}
