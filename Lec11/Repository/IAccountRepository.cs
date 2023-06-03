using Lec11.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Lec11.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> ChangePassword(ChangePassword model);
        Task<IdentityResult> ConfirmEmailAsync(string uid, string token);
        Task GenerateEmailConfirmationTokenAsync(ApplicationUser user);
        Task GeneratePasswordResetTokenAsync(ApplicationUser user);
        Task<ApplicationUser> GetUserByEmailAssync(string email);
        Task<SignInResult> LoginUser(SigninModel model);
        Task LogoutUser();
        Task<IdentityResult> RegisterUser(SignUpUserModel model);
        Task SendConfirmationEmail(ApplicationUser user, string token);
        Task SendPasswordResetEmail(ApplicationUser user, string token);
        Task<IdentityResult> ResetPassword(PasswordResetModel model);
    }
}