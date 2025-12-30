using SaleApi.Models;

namespace SaleApi.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUser();
    }
}