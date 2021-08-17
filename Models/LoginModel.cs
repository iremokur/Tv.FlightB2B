using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Flightb2b.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter agency name.")]
        [Display(Name ="Agencyname")]
        public string Agency { get; set; }

        [Required(ErrorMessage = "Please enter user name.")]
        [Display(Name = "Username")]
        public string User { get; set; }

        [Required(ErrorMessage = "Please enter password.")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


    }
}
