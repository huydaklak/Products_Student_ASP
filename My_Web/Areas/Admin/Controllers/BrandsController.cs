using Microsoft.AspNetCore.Mvc;
using My_Web.Models;

namespace My_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandsController : Controller
    {
        private readonly AppDbContext _context;
        public BrandsController(AppDbContext context) => _context = context;

        public IActionResult Index()
        {

            return View(_context.Brands.ToList());
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Brand brand)
        {
            if (ModelState.IsValid)
            {
                _context.Brands.Add(brand);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(brand);
        }

        public IActionResult Edit(int id)
        {

            var brand = _context.Brands.Find(id);
            return View(brand);
        }

        [HttpPost]
        public IActionResult Edit(Brand brand)
        {
            if (ModelState.IsValid)
            {
                _context.Brands.Update(brand);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(brand);
        }

        public IActionResult Delete(int id)
        {

            var brand = _context.Brands.Find(id);
            return View(brand); // nếu bạn có view xác nhận xóa
        }
        public IActionResult AdminList()
        {
            // Load danh sách sản phẩm cho admin
            return View();
        }
    }
}
