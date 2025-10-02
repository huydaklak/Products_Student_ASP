using My_Web.Models;

namespace My_Web.Interfaces
{
    public interface IOrderService
    {
        int PlaceOrder(string email, OrderViewModel model);
        List<Order> GetOrders(string email);
        Order GetOrderDetails(int orderId);
        int CreateOrder(int productId, string userId);
    }
}
