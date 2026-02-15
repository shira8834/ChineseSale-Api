using Microsoft.EntityFrameworkCore;
using SaleApi.Data;
using SaleApi.Models;

namespace SaleApi.Repositories
{


    public class OrderRepository : IOrderRepository
    {
        private readonly SaleContextDB _context;
        public OrderRepository(SaleContextDB saleContextDB) => _context = saleContextDB;

        //GetAllOrder
        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _context.Orders
                .Include(o => o.Gift)
                .Include(o => o.User) // כאן היה התיקון מ- o ל- User
                .ToListAsync();
        }

        //GetOrdersSortedByPopularity
        public async Task<IEnumerable<Order>> GetOrdersSortedByPopularity()
        {
            return await _context.Orders
                .Include(o => o.Gift)
                .Include(o => o.User)
                .ToListAsync();
        }

        // AddOrder
        public async Task<Order> AddOrder(Order order)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == order.IdUser);
            var giftExists = await _context.Gifts.AnyAsync(g => g.Id == order.IdGift);

            if (!userExists || !giftExists)
                throw new Exception("User or Gift not found in database.");

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        //GetOrdersByGiftId
        public async Task<IEnumerable<Order>> GetOrdersByGiftId(int giftId)
        {
            return await _context.Orders
                .Where(o => o.IdGift == giftId)
                .Include(o => o.User)
                .Include(o => o.Gift)
                .ToListAsync();
        }
    }
}