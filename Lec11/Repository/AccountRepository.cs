using Lec11.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Lec11.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterUser(SignUpUserModel model)
        {
            var user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            return result;

        }

        public async Task<SignInResult> LoginUser(SigninModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Pasword, model.RememberMe, false);
            return result;
        }
    }
}
