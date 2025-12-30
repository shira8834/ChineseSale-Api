using SaleApi.Models;

namespace SaleApi.Repositories
{
    public interface IGiftRepository
    {
        Task DeleteGift(int id);
        Task<IEnumerable<Gift>> GetAllGift();
        Task<Gift?> GetGiftById(int id);
        Task<Doner> GetGiftDoner(int id);
        Task<Gift> NewGift(Gift gift);
        Task<Gift> UpdateGift(Gift gift);

    }
}