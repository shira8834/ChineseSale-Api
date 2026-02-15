using SaleApi.Dto; // חשוב מאוד!
using static SaleApi.Dto.OrderDto; // כדי שיכיר את GetOrderDto ו-AddOrderDto

namespace SaleApi.Services
{
    public interface IOrderService
    {
        //Task<bool> CloseBagToOrder(int userId);
        Task<IEnumerable<GetOrderDto>> GetAllOrders();
        Task<IEnumerable<GetOrderDto>> GetOrdersSortedByPopularity();
        Task<AddOrderDto?> AddOrder(AddOrderDto dto);
        Task<IEnumerable<GetOrderDto>> GetOrdersByGiftId(int giftId);
    }
}