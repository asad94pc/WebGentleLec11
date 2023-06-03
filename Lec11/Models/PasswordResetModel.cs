using System.ComponentModel.DataAnnotations;

namespace Lec11.Models
{
    public class PasswordResetModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Token { get; set; }

        [Required, DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required, DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }

        public bool IsSuccess { get; set; }
    }
}
