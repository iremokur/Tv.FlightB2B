using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Flightb2b.Models
{
    public class FlightModel
    {
        /// 
        /// ONE WAY
      
        [Required]
        [Display(Name = "From")]
        public string Departure { get; set; }

        
        [Display(Name = "To")]
        public string Arrival { get; set; }


     
    }
}
