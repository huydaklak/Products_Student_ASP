using My_Web.Interfaces;
using My_Web.Models;

namespace My_Web.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public List<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public Product GetById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.ProductID == id);
        }

        public List<Product> GetByCategory(int categoryId)
        {
            return _context.Products.Where(p => p.CategoryID == categoryId).ToList();
        }
    }
}
