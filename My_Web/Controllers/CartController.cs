using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Web.Models;

namespace My_Web.Controllers
{
    [Authorize(Roles = "User")]
    public class CartController : BaseController
    {
        private readonly AppDbContext _context;
        public CartController(AppDbContext context) => _context = context;

        public IActionResult Index()
        {
            SetLayout();
            var userId = GetCurrentUserId();
            var cartItems = _context.CartItems
                .Where(c => c.UserID == userId)
                .ToList();
            return View(cartItems);
        }

        public IActionResult AddToCart(int productId)
        {
            var userId = GetCurrentUserId();
            var item = new CartItem
            {
                ProductID = productId,
                UserID = userId,
                Quantity = 1
            };
            _context.CartItems.Add(item);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            var item = _context.CartItems.Find(id);
            if (item != null)
            {
                _context.CartItems.Remove(item);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        private int GetCurrentUserId()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == "UserID");
            return claim != null ? int.Parse(claim.Value) : 0;
        }
        public IActionResult AdminList()
        {
            // Load danh sách sản phẩm cho admin
            return View();
        }
    }
}
