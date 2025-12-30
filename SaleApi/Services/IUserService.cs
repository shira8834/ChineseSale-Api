using SaleApi.Models;

namespace SaleApi.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUser();
    }
}