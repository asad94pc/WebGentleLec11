using Lec11.Models;
using Lec11.Repository;
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
            }
            return View();
        }
        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(SigninModel model)
        {
            var result = await _accountRepository.LoginUser(model);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
                
            }
            ModelState.AddModelError("", "invalid credentials");
            return View(model);
        }





    }
}
