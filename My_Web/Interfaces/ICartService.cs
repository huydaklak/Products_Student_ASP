using My_Web.Models;

namespace My_Web.Interfaces
{
    public interface ICartService
    {
        List<CartItem> GetCart(string email);
        bool AddItem(string email, int productId);
        bool RemoveItem(string email, int productId);
        void ClearCart(string email);
    }
}
