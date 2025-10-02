using Microsoft.EntityFrameworkCore;
using My_Web.Interfaces;
using My_Web.Models;

namespace My_Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        // Đặt hàng từ giỏ
        public int PlaceOrder(int userId, OrderViewModel model)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserID == userId);
            if (user == null) return 0;

            var cartItems = _context.CartItems
                .Where(c => c.UserID == user.UserID)
                .Include(c => c.Product)
                .ToList();

            if (!cartItems.Any()) return 0;

            var order = new Order
            {
                UserID = user.UserID,
                OrderDate = DateTime.Now,
                Status = "Chờ xác nhận",
                TotalAmount = cartItems.Sum(c => c.Product.Price * c.Quantity),
                OrderDetails = cartItems.Select(c => new OrderDetail
                {
                    ProductID = c.ProductID,
                    Quantity = c.Quantity,
                    Price = c.Product.Price
                }).ToList()
            };

            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();

            return order.OrderID;
        }

        // Mua ngay một sản phẩm
        public int CreateOrder(int productId, int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserID == userId);
            if (user == null) return 0;

            var product = _context.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product == null) return 0;

            var order = new Order
            {
                UserID = user.UserID,
                OrderDate = DateTime.Now,
                Status = "Chờ xác nhận",
                TotalAmount = product.Price,
                OrderDetails = new List<OrderDetail>
                {
                    new OrderDetail
                    {
                        ProductID = productId,
                        Quantity = 1,
                        Price = product.Price
                    }
                }
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            return order.OrderID;
        }

        // Lấy danh sách đơn hàng của người dùng
        public List<Order> GetOrders(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserID == userId);
            if (user == null) return new List<Order>();

            return _context.Orders
                .Where(o => o.UserID == user.UserID)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
        }

        // Lấy chi tiết đơn hàng
        public Order? GetOrderDetails(int orderId)
        {
            return _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefault(o => o.OrderID == orderId);
        }
    }
}
