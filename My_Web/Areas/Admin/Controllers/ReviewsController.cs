using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Web.Models;

namespace My_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReviewsController : Controller
    {
        private readonly AppDbContext _context;
        public ReviewsController(AppDbContext context) => _context = context;

        public IActionResult Index()
        {

            var reviews = _context.Reviews
                .Include(r => r.Product)
                .Include(r => r.User)
                .ToList();
            return View(reviews);
        }

        public IActionResult Delete(int id)
        {

            var review = _context.Reviews.Find(id);
            return View(review); // nếu có view xác nhận xóa
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            var review = _context.Reviews.Find(id);
            _context.Reviews.Remove(review);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult AdminList()
        {
            // Load danh sách sản phẩm cho admin
            return View();
        }
    }
}
