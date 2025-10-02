using Microsoft.AspNetCore.Mvc;

namespace My_Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home", new { area = "User" });
        }
    }
}
