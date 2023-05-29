using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace Lec11.Models
{
    public class SigninModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Pasword { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
