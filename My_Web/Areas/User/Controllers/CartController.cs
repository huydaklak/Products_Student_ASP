using Microsoft.AspNetCore.Mvc;
using My_Web.Interfaces;
using My_Web.Models;

namespace My_Web.Areas.User.Controllers
{
    [Area("User")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // ✅ Hiển thị giỏ hàng
        public IActionResult Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                TempData["Error"] = "Vui lòng đăng nhập để xem giỏ hàng!";
                return RedirectToAction("Login", "Account", new { area = "User" });
            }

            var cart = _cartService.GetCart(userId.Value) ?? new List<CartItem>();
            ViewBag.CartCount = cart.Sum(c => c.Quantity);

            ViewBag.Success = TempData["Success"];
            ViewBag.Error = TempData["Error"];

            return View(cart);
        }

        // ✅ Thêm vào giỏ bằng GET
        public IActionResult Add(int productId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login", "Account", new { area = "User" });

            bool result = _cartService.AddItem(userId.Value, productId);

            TempData["Success"] = result
                ? "Đã thêm sản phẩm vào giỏ hàng!"
                : "Không thể thêm sản phẩm. Vui lòng thử lại!";

            return RedirectToAction("Index");
        }

        // ✅ Xóa sản phẩm khỏi giỏ
        public IActionResult Remove(int productId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login", "Account", new { area = "User" });

            bool result = _cartService.RemoveItem(userId.Value, productId);

            TempData["Success"] = result
                ? "Đã xóa sản phẩm khỏi giỏ hàng!"
                : "Không thể xóa sản phẩm. Vui lòng thử lại!";

            return RedirectToAction("Index");
        }

        // ✅ Thêm vào giỏ bằng POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int productId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId"); // nullable int

            if (!userId.HasValue) // check null an toàn
            {
                return RedirectToAction("Login", "Account", new { area = "User" });
            }

            bool result = _cartService.AddItem(userId.Value, productId); // dùng .Value vì chắc chắn có giá trị

            TempData["Success"] = result
                ? "Đã thêm sản phẩm vào giỏ hàng!"
                : "Không thể thêm sản phẩm. Vui lòng thử lại!";

            return RedirectToAction("Index");
        }

    }
}
