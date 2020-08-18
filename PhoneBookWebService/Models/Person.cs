using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBookAppCF.Models
{
    public class Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name ="E-Mail")]
        public string Email { get; set; }

        
        [Required]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [ForeignKey("City")]
        [Display(Name ="City")]
        public int CityID { get; set; }

        [ForeignKey("State")]
        [Display(Name ="State")]
        public int StateID { get; set; }       

        [Display(Name = "Pin Code")]
        [Required]
        public int PinCode { get; set; }

        [ForeignKey("Country")]
        [Display(Name ="Country")]
        public int CountryID { get; set; }

        public virtual State State { get; set; }
        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public bool IsActive { get; set; } = true;


    }
}