using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBookAppCF.Models
{
    public class Country
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CountryID { get; set; }

        [Display(Name ="Country")]
        [Required]
        public string CountryName { get; set; }
        public bool IsActive { get; set; } = true;
        public virtual ICollection<State> States { get; set; }
        
    }
}