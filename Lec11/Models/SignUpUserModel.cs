using System;
using System.ComponentModel.DataAnnotations;

namespace Lec11.Models
{
    public class SignUpUserModel
    {
        [Required(ErrorMessage = "Please enter First Name")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Please enter your email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Pasword is required")]
        [Compare("ConfirmPassword", ErrorMessage = "Passwords do not match")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter same messgae for confirnation")]
        [Display(Name = "Conifm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set;}
    }
}
