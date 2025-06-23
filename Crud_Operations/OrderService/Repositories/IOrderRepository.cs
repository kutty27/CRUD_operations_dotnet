using OrderService.Models;

namespace OrderService.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order> GetOrderById(int id);
        Task<List<Order>> GetOrderByProductId(int id);
        Task<Order> AddOrder(Order order);
        Task<Order> UpdateOrder(Order order);
        Task<bool> DeleteOrder(int id);
    }
}
