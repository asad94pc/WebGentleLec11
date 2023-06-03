using System.ComponentModel.DataAnnotations;

namespace Lec11.Models
{
    public class ChangePassword
    {
        [Required]
        [DataType(DataType.Password, ErrorMessage = "Current Password is required")]
        public string CurrentPassword { get; set; }
        
        [Required]
        [DataType(DataType.Password, ErrorMessage = "NewPassword is required")]
        public string NewPassword { get; set; }
        
        [Required]
        [DataType(DataType.Password, ErrorMessage = "ConfirmNewPassword is required")]
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
    }
}
