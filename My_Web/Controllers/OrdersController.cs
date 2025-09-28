using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Web.Models;

namespace My_Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;
        public OrdersController(AppDbContext context) => _context = context;

        public IActionResult AdminList()
        {
            var orders = _context.Orders.Include(o => o.User).ToList();
            return View(orders);
        }


        public IActionResult Edit(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null) return NotFound();
            return View(order);
        }

        [HttpPost]
        public IActionResult Edit(Order updatedOrder)
        {
            var order = _context.Orders.Find(updatedOrder.OrderID);
            if (order == null) return NotFound();

            order.Status = updatedOrder.Status;
            _context.SaveChanges();
            TempData["Success"] = "Cập nhật trạng thái thành công!";
            return RedirectToAction("AdminList");
        }

        public IActionResult Delete(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null || !order.CanCancel) return BadRequest("Không thể hủy đơn này.");
            return View(order);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null || !order.CanCancel) return BadRequest("Không thể hủy đơn này.");

            _context.Orders.Remove(order);
            _context.SaveChanges();
            TempData["Success"] = "Đã hủy đơn hàng!";
            return RedirectToAction("AdminList");
        }
        public IActionResult Details(int id)
        {
            var order = _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Include(o => o.User)
                .FirstOrDefault(o => o.OrderID == id);

            if (order == null) return NotFound();
            return View(order);
        }
    }
}
