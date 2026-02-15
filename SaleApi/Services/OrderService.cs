using SaleApi.Dto;
using SaleApi.Models;
using SaleApi.Repositories;
using static SaleApi.Dto.OrderDto;

namespace SaleApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBagRepository _bagRepository;

        public OrderService(IOrderRepository orderRepository, IBagRepository bagRepository)
        {
            _orderRepository = orderRepository;
            _bagRepository = bagRepository;
        }

        //public async Task<bool> CloseBagToOrder(int userId)
        //{
        //    var itemsInBag = await _bagRepository.GetBagByUser(userId);
        //    if (itemsInBag == null || !itemsInBag.Any()) return false;

        //    foreach (var item in itemsInBag)
        //    {
        //        var newOrder = new Order { IdUser = userId, IdGift = item.IdGift, Win = false };
        //        await _orderRepository.AddOrder(newOrder);
        //    }

        //    await _bagRepository.ClearUserBag(userId);
        //    return true;
        //}

        // מימוש שאר הפונקציות כדי שהקו האדום תחת שם ה-class יעלם
        public async Task<IEnumerable<GetOrderDto>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllOrders();
            return orders.Select(o => MapToDto(o));
        }

        public async Task<IEnumerable<GetOrderDto>> GetOrdersSortedByPopularity()
        {
            var orders = await _orderRepository.GetOrdersSortedByPopularity();
            return orders.Select(o => MapToDto(o));
        }

        public async Task<AddOrderDto?> AddOrder(AddOrderDto dto)
        {
            var order = new Order { IdUser = dto.IdUser, IdGift = dto.IdGift, Win = false };
            var created = await _orderRepository.AddOrder(order);
            return created != null ? dto : null;
        }

        public async Task<IEnumerable<GetOrderDto>> GetOrdersByGiftId(int giftId)
        {
            var orders = await _orderRepository.GetOrdersByGiftId(giftId);
            return orders.Select(o => MapToDto(o));
        }

        private GetOrderDto MapToDto(Order o) => new GetOrderDto
        {
            Id = o.Id,
            IdUser = o.IdUser,
            Win = o.Win,
            User = o.User != null ? new UserShortDto { FirstName = o.User.FirstName, LastName = o.User.LastName } : null,
            Gift = o.Gift != null ? new OrderShortDto { Id = o.Gift.Id, Name = o.Gift.Name, Price = (int)o.Gift.Price } : null
        };
    }
}