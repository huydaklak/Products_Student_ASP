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
            var email = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(email))
            {
                TempData["Error"] = "Vui lòng đăng nhập để thanh toán!";
                return RedirectToAction("Login", "Account");
            }

            var cart = _cartService.GetCart(email) ?? new List<CartItem>();
            if (cart.Count == 0)
            {
                TempData["Error"] = "Giỏ hàng của bạn đang trống!";
                return RedirectToAction("Index", "Cart");
            }

            return View(cart);
        }

        // Đặt hàng từ giỏ
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PlaceOrder(OrderViewModel model)
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login", "Account");

            var orderId = _orderService.PlaceOrder(email, model);

            TempData["Success"] = "Đặt hàng thành công!";
            return RedirectToAction("Confirmation", new { id = orderId });
        }

        // Xác nhận (chi tiết) đơn hàng
        public IActionResult Confirmation(int id)
        {
            var order = _orderService.GetOrderDetails(id); // dùng service
            if (order == null)
            {
                TempData["Error"] = "Không tìm thấy đơn hàng!";
                return RedirectToAction("History");
            }

            return View(order);
        }

        // Lịch sử đơn hàng
        public IActionResult History()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login", "Account");

            var orders = _orderService.GetOrders(email);
            return View(orders);
        }

        // Mua ngay 1 sản phẩm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BuyNow(int productId)
        {
            // CHÚ Ý: OrderService.CreateOrder hiện tại của bạn nhận (int productId, string userId)
            // và parse userId thành int. Vì vậy ở đây ta sẽ lấy "UserId" từ Session (lưu trước khi login).
            var userId = HttpContext.Session.GetString("UserId"); // đây là string id (vd "5")
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Account");

            var orderId = _orderService.CreateOrder(productId, userId);
            return RedirectToAction("Confirmation", new { id = orderId });
        }

        // Details (dùng service)
        public IActionResult Details(int id)
        {
            var order = _orderService.GetOrderDetails(id);
            if (order == null) return NotFound();

            return View(order);
        }
    }
}
