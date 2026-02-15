using SaleApi.Models;

namespace SaleApi.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<IEnumerable<Order>> GetOrdersSortedByPopularity();
        Task<Order?> AddOrder(Order order);
        Task<IEnumerable<Order>> GetOrdersByGiftId(int giftId);
    }
}
