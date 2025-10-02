using My_Web.Models;
namespace My_Web.Interfaces
{
    public interface IProductService
    {
        List<Product> GetAll();
        Product GetById(int id);
        List<Product> GetByCategory(int categoryId);
    }
}
