using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Web.Models;

namespace My_Web.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            SetLayout();
            if (User.IsInRole("Admin"))
                ViewData["Layout"] = "~/Views/Shared/_LayoutAdmin.cshtml";
            else
                ViewData["Layout"] = "~/Views/Shared/_LayoutUser.cshtml";
            var products = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null && Image.Length > 0)
                {
                    var fileName = Path.GetFileName(Image.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);
                    }

                    product.Image = "/images/" + fileName;
                }

                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("AdminList");
            }

            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            return View(product);
        }

        public IActionResult Edit(int id)
        {
            if (User.IsInRole("Admin"))
                ViewData["Layout"] = "~/Views/Shared/_LayoutAdmin.cshtml";
            else
                ViewData["Layout"] = "~/Views/Shared/_LayoutUser.cshtml";

            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product, IFormFile Image)
        {
            if (User.IsInRole("Admin"))
                ViewData["Layout"] = "~/Views/Shared/_LayoutAdmin.cshtml";
            else
                ViewData["Layout"] = "~/Views/Shared/_LayoutUser.cshtml";

            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Categories = _context.Categories.ToList();

            if (!ModelState.IsValid)
            {
                return View(product);
            }

            var existingProduct = await _context.Products.FindAsync(product.ProductID);
            if (existingProduct == null) return NotFound();

            // Cập nhật thông tin
            existingProduct.ProductName = product.ProductName;
            existingProduct.Price = product.Price;
            existingProduct.Quantity = product.Quantity;
            existingProduct.BrandID = product.BrandID;
            existingProduct.CategoryID = product.CategoryID;

            // Nếu có ảnh mới thì cập nhật, nếu không thì giữ nguyên
            if (Image != null && Image.Length > 0)
            {
                var fileName = Path.GetFileName(Image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }

                existingProduct.Image = "/images/" + fileName;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("AdminList");
        }


        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            var product = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefault(p => p.ProductID == id);

            if (product == null) return NotFound();

            return View(product);
        }

        public IActionResult UserList()
        {
            SetLayout();
            var products = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .ToList();

            return View(products);
        }
        public IActionResult AdminList()
        {
            var products = _context.Products
       .Include(p => p.Brand)
       .Include(p => p.Category)
       .ToList();

            return View(products); // ← phải truyền đúng kiểu
        }
        public IActionResult ListByCategory(int id)
        {
            var products = _context.Products.Where(p => p.CategoryID == id).ToList();
            return View(products);
        }
    }
}
