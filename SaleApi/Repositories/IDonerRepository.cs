using SaleApi.Models;

namespace SaleApi.Repositories
{
    public interface IDonerRepository
    {
        Task AddGiftToDoner(int donerId, Gift gift);
        Task DeleteDoner(int id);
        Task<IEnumerable<Doner>> GetAllDoner();
        Task<Doner?> GetDonerById(int id);
        Task<Doner> NewDoner(Doner doner);
        Task<Doner> UpdateDoner(Doner doner);
    }
}