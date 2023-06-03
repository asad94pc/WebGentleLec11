namespace Lec11.Models
{
    public class EmailConfirmModel
    {
        public string Email { get; set; }
        public bool isEmailConfirmed { get; set; }
        public bool EmailSent { get; set; }
        public bool EmailVerified { get; set; }
    }
}
