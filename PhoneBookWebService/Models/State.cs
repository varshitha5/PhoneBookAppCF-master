using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBookAppCF.Models
{
    public class State
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int StateID { get; set; }

        [Display(Name ="State")]
        [Required]
        public string StateName { get; set; }
        public bool IsActive { get; set; } = true;

        [ForeignKey("Country")]
        public int CountryID { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<City> Cities { get; set; }
       
    }
}