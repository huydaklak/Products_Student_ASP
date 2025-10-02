using My_Web.Models;

namespace My_Web.Interfaces
{
    public interface IOrderService
    {
        int PlaceOrder(int userId, OrderViewModel model);
        List<Order> GetOrders(int userId);
        Order GetOrderDetails(int orderId);
        int CreateOrder(int productId, int userId);
    }
}
