
namespace SaleApi.Services
{
    public interface IRandomService
    {
        Task<int?> PickWinner(int giftId);
    }
}