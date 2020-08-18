using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBookAppCF.Models
{
    public class City
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]

        public int CityID { get; set; }

        [Display(Name ="City Name")]
        public string CityName { get; set; }

        [ForeignKey("State")]
        public int StateID { get; set; }
        public bool IsActive { get; set; } = true;             
        public virtual State State { get; set; }
       
    }
}