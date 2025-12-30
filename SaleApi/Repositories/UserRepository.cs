using Microsoft.EntityFrameworkCore;
using SaleApi.Data;
using SaleApi.Models;

namespace SaleApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        SaleContextDB _context;
        public UserRepository(SaleContextDB saleContextDB)
        {
            _context = saleContextDB;
        }


        ///כל המשתמשים
        public async Task<IEnumerable<User>> GetAllUser()
        {
            return await _context.Users.ToListAsync();
        }


    }
}
