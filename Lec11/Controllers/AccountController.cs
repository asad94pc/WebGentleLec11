using Lec11.Models;
using Lec11.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lec11.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [Route("signup")]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUp(SignUpUserModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.RegisterUser(model);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);

                    }

                    return View(model);
                }

                ModelState.Clear();
                return RedirectToAction("ConfirmEmail", new { email = model.Email });
            }
            //return RedirectToAction("Login", "Account");
            return View(model);
        }
        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(SigninModel model, string returnUrl)
        {
            var result = await _accountRepository.LoginUser(model);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");

            }
            else if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "User Account Blocked: Try again after some time");

            }
            else if (result.IsNotAllowed)
            {
                ModelState.AddModelError("", "Email not confirmed yet");

            }
            else
            {

                ModelState.AddModelError("", "invalid credentials");
            }

            return View(model);
        }
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountRepository.LogoutUser();
            return RedirectToAction("Index", "Home");
        }

        [Route("PasswordChange")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Route("PasswordChange")]
        public async Task<IActionResult> ChangePassword(ChangePassword model)
        {
            var result = await _accountRepository.ChangePassword(model);

            if (result.Succeeded)
            {
                ModelState.Clear();
                ViewBag.PasswordChanged = true;
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        [HttpGet("Email-Confirmation")]
        public async Task<IActionResult> ConfirmEmail(string uid, string token, string email)
        {
            EmailConfirmModel model = new EmailConfirmModel()
            {
                Email = email
            };
            if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(token))
            {
                token = token.Replace(' ', '+');
                var result = await _accountRepository.ConfirmEmailAsync(uid, token);
                if (result.Succeeded)
                {
                    model.EmailVerified = true;
                }

            }
            return View(model);
        }

        [HttpPost("Email-Confirmation")]
        public async Task<IActionResult> ConfirmEmail(EmailConfirmModel emailConfirmModel)
        {
            var user = await _accountRepository.GetUserByEmailAssync(emailConfirmModel.Email);
            if (user != null)
            {
                if (user.EmailConfirmed)
                {
                    emailConfirmModel.isEmailConfirmed = true;
                    return View(emailConfirmModel);
                }
                await _accountRepository.GenerateEmailConfirmationTokenAsync(user);
                emailConfirmModel.EmailSent = true;
                ModelState.Clear();
            }
            else
            {
                ModelState.AddModelError("", "something went wrong");
            }
            return View(emailConfirmModel);
        }

        [AllowAnonymous, HttpGet("forgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [AllowAnonymous, HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotEmailModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _accountRepository.GetUserByEmailAssync(model.Email);
                if (user != null)
                {
                    await _accountRepository.GeneratePasswordResetTokenAsync(user);
                }

                ModelState.Clear();
                model.EmailSent = true;
            }
            return View(model);
        }


        [AllowAnonymous, HttpGet("reset-password")]
        public IActionResult ResetPassword(string uid, string token)
        {
            PasswordResetModel model = new PasswordResetModel()
            {
                Token = token,
                UserId = uid
            };
            return View(model);
        }


        [AllowAnonymous, HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(PasswordResetModel model)
        {
            if (ModelState.IsValid)
            {
                model.Token = model.Token.Replace(' ', '+');
                var result = await _accountRepository.ResetPassword(model);
                if (result.Succeeded)
                {

                    ModelState.Clear();
                    model.IsSuccess = true;
                    return View(model);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }


    }
}
