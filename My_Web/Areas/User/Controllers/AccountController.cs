using Microsoft.AspNetCore.Mvc;
using My_Web.Models;

namespace My_Web.Areas.User.Controllers
{
    [Area("User")]
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Trang login
        public IActionResult Login()
        {
            return View();
        }

        // ✅ Xử lý login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string Email, string Password)
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ thông tin.";
                return View();
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == Email && u.Password == Password);
            if (user == null)
            {
                ViewBag.Error = "Sai email hoặc mật khẩu.";
                return View();
            }

            // Lưu session
            HttpContext.Session.SetString("UserId", user.UserID.ToString());
            HttpContext.Session.SetString("UserEmail", user.Email);

            return RedirectToAction("Index", "Home");
        }

        // ✅ Trang đăng ký
        public IActionResult Register()
        {
            return View();
        }

        // ✅ Xử lý đăng ký
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(string FullName, string Email, string Password)
        {
            if (string.IsNullOrEmpty(FullName) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ thông tin.";
                return View();
            }

            var exists = _context.Users.Any(u => u.Email == Email);
            if (exists)
            {
                ViewBag.Error = "Email đã tồn tại.";
                return View();
            }

            var user = new My_Web.Models.User
            {
                FullName = FullName,
                Email = Email,
                Password = Password,
                Role = "User" // nếu muốn mặc định khi đăng ký
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            // Đăng nhập luôn sau khi đăng ký
            HttpContext.Session.SetString("UserId", user.UserID.ToString());
            HttpContext.Session.SetString("UserEmail", user.Email);

            return RedirectToAction("Index", "Home");
        }

        // ✅ Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        public IActionResult Profile()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(email)) return RedirectToAction("Login");

            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null) return RedirectToAction("Login");

            return View(user); // gọi tới Views/Account/Profile.cshtml
        }
    }
}
