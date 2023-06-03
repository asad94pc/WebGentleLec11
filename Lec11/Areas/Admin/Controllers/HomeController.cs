using Microsoft.AspNetCore.Mvc;

namespace Lec11.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details()
        {
            return View();
        }
    }
}
