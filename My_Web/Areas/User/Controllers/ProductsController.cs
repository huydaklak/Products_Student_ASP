using Microsoft.AspNetCore.Mvc;
using My_Web.Interfaces;
namespace My_Web.Areas.User.Controllers
{
    [Area("User")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var products = _productService.GetAll();
            ViewBag.Products = products;
            return View();
        }

        public IActionResult Details(int id)
        {
            var product = _productService.GetById(id);
            if (product == null) return NotFound();

            return View(product);
        }

        public IActionResult ListByCategory(int id)
        {
            var products = _productService.GetByCategory(id);
            return View(products);
        }
    }
}
