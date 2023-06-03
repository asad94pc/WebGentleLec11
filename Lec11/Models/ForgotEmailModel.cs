using System.ComponentModel.DataAnnotations;

namespace Lec11.Models
{
    public class ForgotEmailModel
    {
        [Required, EmailAddress, Display(Name = "Registered Email")]
        public string Email { get; set; }
        public bool EmailSent { get; set; }
    }
}
