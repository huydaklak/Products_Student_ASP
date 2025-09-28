using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Web.Models;

namespace My_Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class InventoryController : BaseController
    {
        private readonly AppDbContext _context;
        public InventoryController(AppDbContext context) => _context = context;

        public IActionResult Index()
        {
            SetLayout();
            var inventory = _context.Inventories.ToList();
            return View(inventory);
        }

        public IActionResult Edit(int id)
        {
            SetLayout();
            var item = _context.Inventories.Find(id);
            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(Inventory item)
        {
            if (ModelState.IsValid)
            {
                _context.Inventories.Update(item);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            SetLayout();
            return View(item);
        }
        public IActionResult AdminList()
        {
            // Load danh sách sản phẩm cho admin
            return View();
        }
    }
}
