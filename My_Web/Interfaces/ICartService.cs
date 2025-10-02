using My_Web.Models;

namespace My_Web.Interfaces
{
    public interface ICartService
    {
        List<CartItem> GetCart(int userId);
        bool AddItem(int userId, int productId);
        bool RemoveItem(int userId, int productId);
        void ClearCart(int userId);
    }
}
