using SaleApi.Dto;
using SaleApi.Models;
using static SaleApi.Dto.GiftDto;

namespace SaleApi.Services
{
    public interface IGiftService
    {
        Task<IEnumerable<GetGiftDto>> GetAllGift();
        Task<GiftDto.CreateGiftDto> NewGift(GiftDto.CreateGiftDto giftDto);
        Task DeletGift(int id);
         Task<Gift> GetGiftById(int id);
        Task<UpdateGiftDto> UpdateGift(UpdateGiftDto giftDto);
        Task<GiftDonerDto> GetGiftDoner(int id);


    }
}