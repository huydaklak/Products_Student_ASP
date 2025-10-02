using Microsoft.AspNetCore.Mvc;
using My_Web.Interfaces;
using My_Web.Models;

namespace My_Web.Areas.User.Controllers
{
    [Area("User")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;

        public OrderController(IOrderService orderService, ICartService cartService)
        {
            _orderService = orderService;
            _cartService = cartService;
        }

        // Trang thanh toán
        public IActionResult Checkout()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                TempData["Error"] = "Vui lòng đăng nhập để thanh toán!";
                return RedirectToAction("Login", "Account", new { area = "User" });
            }

            var cart = _cartService.GetCart(userId.Value) ?? new List<CartItem>();
            if (cart.Count == 0)
            {
                TempData["Error"] = "Giỏ hàng của bạn đang trống!";
                return RedirectToAction("Index", "Cart", new { area = "User" });
            }

            return View(cart);
        }

        // Đặt hàng từ giỏ
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PlaceOrder(OrderViewModel model)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
                return RedirectToAction("Login", "Account", new { area = "User" });

            var orderId = _orderService.PlaceOrder(userId.Value, model);

            TempData["Success"] = "Đặt hàng thành công!";
            return RedirectToAction("Confirmation", new { id = orderId });
        }

        // Xác nhận đơn hàng
        public IActionResult Confirmation(int id)
        {
            var order = _orderService.GetOrderDetails(id);
            if (order == null)
            {
                TempData["Error"] = "Không tìm thấy đơn hàng!";
                return RedirectToAction("History", new { area = "User" });
            }

            return View(order);
        }

        // Lịch sử đơn hàng
        public IActionResult History()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
                return RedirectToAction("Login", "Account", new { area = "User" });

            var orders = _orderService.GetOrders(userId.Value);
            return View(orders);
        }

        // Mua ngay 1 sản phẩm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BuyNow(int productId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
                return RedirectToAction("Login", "Account", new { area = "User" });

            var orderId = _orderService.CreateOrder(productId, userId.Value);
            return RedirectToAction("Confirmation", new { id = orderId });
        }

        // Chi tiết đơn hàng
        public IActionResult Details(int id)
        {
            var order = _orderService.GetOrderDetails(id);
            if (order == null) return NotFound();

            return View(order);
        }
    }
}
