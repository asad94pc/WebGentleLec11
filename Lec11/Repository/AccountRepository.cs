using Lec11.Models;
using Lec11.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lec11.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserIdentityServices _userIdentityServices;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configOptions;

        public AccountRepository(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IUserIdentityServices userIdentityServices,
            IEmailService emailService,
            IConfiguration configOptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userIdentityServices = userIdentityServices;
            _emailService = emailService;
            _configOptions = configOptions;
        }

        public async Task<ApplicationUser> GetUserByEmailAssync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
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

            if (result.Succeeded)
            {
                await GenerateEmailConfirmationTokenAsync(user);

                //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //if (!string.IsNullOrEmpty(token))
                //{

                //    await SendConfirmationEmail(user, token);
                //}
            }

            return result;

        }

        public async Task GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendConfirmationEmail(user, token);
            }
        }
        public async Task GeneratePasswordResetTokenAsync(ApplicationUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendPasswordResetEmail(user, token);
            }
        }

        public async Task<SignInResult> LoginUser(SigninModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Pasword, model.RememberMe, true);
            return result;
        }

        public async Task LogoutUser()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> ChangePassword(ChangePassword model)
        {
            var userId = _userIdentityServices.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            return await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        }
        public async Task<IdentityResult> ResetPassword(PasswordResetModel model)
        {
            return await _userManager.ResetPasswordAsync(await _userManager.FindByIdAsync(model.UserId), model.Token, model.NewPassword);

        }
        public async Task SendConfirmationEmail(ApplicationUser user, string token)
        {
            string appDomain = _configOptions.GetSection("MyLoginPath:AppDomain").Value;
            string confirmLink = _configOptions.GetSection("MyLoginPath:EmailConfirmation").Value;

            UserEmailOptions options = new UserEmailOptions()
            {
                ToEmails = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.FirstName),
                    new KeyValuePair<string, string>("{{Link}}", string.Format(appDomain + confirmLink, user.Id, token)),
                }
            };

            await _emailService.SendConfirmEmail(options);

        }
        public async Task SendPasswordResetEmail(ApplicationUser user, string token)
        {
            string appDomain = _configOptions.GetSection("MyLoginPath:AppDomain").Value;
            string confirmLink = _configOptions.GetSection("MyLoginPath:PasswordReset").Value;

            UserEmailOptions options = new UserEmailOptions()
            {
                ToEmails = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.FirstName),
                    new KeyValuePair<string, string>("{{Link}}", string.Format(appDomain + confirmLink, user.Id, token)),
                }
            };

            await _emailService.SendForgotPasswordEmail(options);

        }
        public async Task<IdentityResult> ConfirmEmailAsync(string uid, string token)
        {
            return await _userManager.ConfirmEmailAsync(await _userManager.FindByIdAsync(uid), token);
        }
    }
}
