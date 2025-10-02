using Microsoft.AspNetCore.Mvc;
using My_Web.Models;

namespace My_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;
        public UsersController(AppDbContext context) => _context = context;

        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(My_Web.Models.User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                TempData["Success"] = "Thêm người dùng thành công!";
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public IActionResult Edit(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(My_Web.Models.User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.Users.Find(user.UserID);
                if (existingUser == null) return NotFound();

                existingUser.FullName = user.FullName;
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;
                existingUser.Role = user.Role;

                _context.SaveChanges();
                TempData["Success"] = "Cập nhật người dùng thành công!";
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();
            return View(user);
        }


        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            _context.SaveChanges();
            TempData["Success"] = "Xóa người dùng thành công!";
            return RedirectToAction("Index");
        }
        public IActionResult AdminList()
        {
            var users = _context.Users.ToList();
            return View(users); // ← cần có file AdminList.cshtml
        }
    }
}
