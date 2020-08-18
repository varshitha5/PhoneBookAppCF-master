namespace PhoneBook.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Address")]
    public partial class Address
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Required]
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public int CityID { get; set; }

        public int StateID { get; set; }

        public int PinCode { get; set; }

        public int CountryID { get; set; }

        public virtual City City { get; set; }

        public virtual Country Country { get; set; }

        public virtual Person Person { get; set; }

        public virtual State State { get; set; }
    }
}
