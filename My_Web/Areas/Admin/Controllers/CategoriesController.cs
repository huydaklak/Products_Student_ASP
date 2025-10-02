using Microsoft.AspNetCore.Mvc;
using My_Web.Models;

namespace My_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _context;
        public CategoriesController(AppDbContext context) => _context = context;

        public IActionResult Index()
        {

            return View(_context.Categories.ToList());
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        public IActionResult Edit(int id)
        {

            var category = _context.Categories.Find(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        public IActionResult Delete(int id)
        {

            var category = _context.Categories.Find(id);
            return View(category); // nếu có view xác nhận xóa
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult AdminList()
        {
            // Load danh sách sản phẩm cho admin
            return View();
        }
    }
}
