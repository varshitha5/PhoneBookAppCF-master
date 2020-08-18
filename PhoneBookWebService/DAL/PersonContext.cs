using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhoneBookAppCF.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PhoneBookAppCF.DAL
{
    public class PersonContext : DbContext
    {
        public PersonContext() : base("name = PersonContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PersonContext, Migrations.Configuration>());
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Country> Countries { get; set; }

        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Person>()
      .Map(map =>
      {
          map.Properties(p => new
          {
              p.ID,
              p.FirstName,
              p.LastName,
              p.PhoneNumber,
              p.Email,
              p.IsActive

          });
          map.ToTable("Person");
      })

      .Map(map =>
      {
          map.Properties(p => new
          {
              p.AddressLine1,
              p.AddressLine2,
              p.CityID,
              p.StateID,
              p.CountryID,
              p.PinCode
          });
          map.ToTable("Address");
      });
        }
        public override int SaveChanges()
        {
            var Changed = ChangeTracker.Entries();
            if (Changed != null)
            {
                foreach (var entry in Changed.Where(e => e.State == EntityState.Deleted))
                {
                    entry.State = EntityState.Modified;
                    entry.CurrentValues["IsActive"] = false;
                }
            }
            return base.SaveChanges();
        }
    }
}