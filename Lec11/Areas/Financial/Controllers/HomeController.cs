using Microsoft.AspNetCore.Mvc;

namespace Lec11.Areas.Financial.Controllers
{
    [Area("Financial")]
    //[Route("area/[controller]/[action]")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
