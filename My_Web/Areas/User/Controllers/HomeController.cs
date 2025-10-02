using Microsoft.AspNetCore.Mvc;
using My_Web.Interfaces;

namespace My_Web.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var products = _productService.GetAll();
            ViewBag.Products = products;
            return View();
        }
    }
}
