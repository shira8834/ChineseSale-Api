using Microsoft.EntityFrameworkCore;
using SaleApi.Models;
using SaleApi.Repositories;
using static SaleApi.Dto.DonerDto;
using static SaleApi.Dto.GiftDto;

namespace SaleApi.Services
{
    public class GiftService : IGiftService
    {
        private readonly IGiftRepository _giftRepository;

        public GiftService(IGiftRepository giftRepository)
        {
            _giftRepository = giftRepository;
        }
        //כל המתנות 
        public async Task<IEnumerable<GetGiftDto>> GetAllGift()
        {

            var gifts = await _giftRepository.GetAllGift();

            var giftDtos = gifts.Select(g => new GetGiftDto
            {
                Id = g.Id,
                Name = g.Name,
                Description = g.Description,
                Price = g.Price,
                IdDoner=g.IdDoner,
                Doner=g.Doner
            });

            return giftDtos;
        }




        //מתנה חדשה
        public async Task<CreateGiftDto> NewGift(CreateGiftDto giftDto)
        {
            var gift = new Gift
            {
                Name = giftDto.Name,
                Description = giftDto.Description,
                Img = giftDto.Img,
                Price = giftDto.Price,
                IdDoner = giftDto.IdDoner,
                //    CategoryId=giftDto.CategoryId,

            };
            var cerated = await _giftRepository.NewGift(gift);
            return new CreateGiftDto
            {
                Name = cerated.Name,
                Description = cerated.Description,
                Img = cerated.Img,
                Price = cerated.Price,
                IdDoner = cerated.IdDoner,
                //  CategoryId = cerated.CategoryId,
            };
        }



        //מחיקת מתנה
        public async Task DeletGift(int id)
        {
            await _giftRepository.DeleteGift(id);
        }


        //GetGiftById
        public async Task<Gift> GetGiftById(int id)
        {
            var g = await _giftRepository.GetGiftById(id);
            if (g == null) return null;
            return g;
        }


        //עידכון מתנה
        public async Task<UpdateGiftDto> UpdateGift(UpdateGiftDto giftDto)
        {
            var existing = await _giftRepository.GetGiftById(giftDto.Id);
            if (existing == null) return null;

            existing.Name = giftDto.Name ?? existing.Name;
            existing.Description = giftDto.Description ?? existing.Description;
            existing.Img = giftDto.Img ?? existing.Img;
            if (giftDto.Price > 0)
            {
                existing.Price = giftDto.Price;
            }

            // existing.IdDoner = giftDto.IdDoner ?? existing.IdDoner;

            var updatedGift = await _giftRepository.UpdateGift(existing);
            if (updatedGift == null) return null;
            return new UpdateGiftDto
            { Id = updatedGift.Id, Name = updatedGift.Name, Img = updatedGift.Img, Description = updatedGift.Description, Price = updatedGift.Price };

        }


        // מי התורם 
        public async Task<GiftDonerDto> GetGiftDoner(int id)
        {
            var donerEntity = await _giftRepository.GetGiftDoner(id);
            if (donerEntity == null)
                return null;

            return new GiftDonerDto
            {
                Id = donerEntity.Id,
                FirstName = donerEntity.FirstName,
                LastName=donerEntity.LastName,
                EMail=donerEntity.Email
            };

            //return await _giftRepository.GetGiftDoner(id);


        }






    }
}
