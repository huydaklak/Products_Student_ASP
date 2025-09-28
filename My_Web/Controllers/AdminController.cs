using Microsoft.AspNetCore.Mvc;

namespace My_Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
