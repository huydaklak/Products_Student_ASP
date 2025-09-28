using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Web.Models;

namespace My_Web.Controllers
{
    public class SupportController : Controller
    {
        private readonly AppDbContext _context;
        public SupportController(AppDbContext context) => _context = context;

        public IActionResult Index()
        {
            var messages = _context.Messages
                .Include(m => m.User)
                .OrderByDescending(m => m.Timestamp)
                .ToList();

            return View(messages);
        }

        public IActionResult SendMessage()
        {
            return View(new Message());
        }

        [HttpPost]
        public IActionResult SendMessage(Message message)
        {
            if (!ModelState.IsValid)
            {
                return View(message);
            }

            // Nếu dùng session:
            // message.UserID = int.Parse(HttpContext.Session.GetString("UserID"));

            message.Timestamp = DateTime.Now;
            _context.Messages.Add(message);
            _context.SaveChanges();

            TempData["Success"] = "Tin nhắn đã được gửi!";
            return RedirectToAction("SendMessage");
        }
        public IActionResult AdminList()
        {
            var messages = _context.Messages
        .Include(m => m.User)
        .OrderByDescending(m => m.Timestamp)
        .ToList();

            return View(messages);
        }
    }
}
