using Microsoft.EntityFrameworkCore;
using SaleApi.Models;
using SaleApi.Repositories;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static SaleApi.Dto.BagDto;
using static SaleApi.Dto.GiftDto;

namespace SaleApi.Services
{
    public class BagService : IBagService
    {
        private readonly IBagRepository _bagRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IRandomRepository _randomRepository;
        //private readonly IGiftRepository _giftRepository;
        private readonly ILogger<BagService> _logger;

        public BagService(IBagRepository bagRepository, IOrderRepository orderRepository, IRandomRepository randomRepository)
        {
            _bagRepository = bagRepository;
            _orderRepository = orderRepository;
            _randomRepository= randomRepository;
        }
        //לקנייה
        public async Task<bool> ProcessCheckout(int userId)
        {
            var itemsInBag = await _bagRepository.GetBagByUser(userId);
            if (itemsInBag == null || !itemsInBag.Any()) return false;

            // יוצר מספר ייחודי לכל לחיצה על Checkout
            int newGroupId = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

            foreach (var item in itemsInBag)
            {
                for (int i = 0; i < item.Quantity; i++)
                {
                    var newOrder = new Order
                    {
                        IdUser = userId,
                        IdGift = item.IdGift,
                        Win = false,
                        OrderGroupId = newGroupId // השורה הקריטית!
                    };
                    await _orderRepository.AddOrder(newOrder);
                }
            }
            await _bagRepository.ClearUserBag(userId);
            return true;
        }

        //כל הסלים
        public async Task<IEnumerable<GetBagDto>> GetAllBag()
        {
            var allBags = await _bagRepository.GetAllBag();
            var bagDto = allBags
                .Where(b => b.Gift != null)
    .Select(b => new GetBagDto
    {
        Id = b.Id,
        IdUser = b.IdUser,
        IdGift = b.IdGift,
        Gift = new GiftResponseDto
        {
            Id = b.Id,
            Name = b.Gift.Name,
            Description = b.Gift.Description,
            Img = b.Gift.Img,
            Price = b.Gift.Price,
            IdDoner = b.Gift.IdDoner,

        }
    })
    .ToList();
            return bagDto;
        }


        //סל חדש
        public async Task<Bag> NewGiftToBag(CreateBagDto bagDto)
        {
            var bag = new Bag
            {
                IdUser = bagDto.IdUser,
                IdGift = bagDto.IdGift,
                Quantity = bagDto.Quantity
            };

            var result = await _bagRepository.NewGiftToBag(bag);

            if (result == null)
            {
                throw new Exception("המוצר לא נמצא במערכת");
            }

            bool isDrawn = await _randomRepository.IsGiftDrawnAsync(bag.IdGift);

            if (isDrawn)
            {
                throw new Exception("לא ניתן להוסיף לסל: מתנה זו כבר הוגרלה!");
            }

            return result; 
        }
        //מחיקה מהסל

        public async Task DeleteBag(int id)
        {
            await _bagRepository.DeleteBag(id);
        }

        //GetBagById
        public async Task<Bag> GetBagById(int id)
        {
            var b = await _bagRepository.GetBagById(id);
            if (b == null) return null;
            return b;
        }

        //שולף מתנות לפי משתמש
        public async Task<IEnumerable<Bag>> GetBagByUser(int id)
        {
            var b = await _bagRepository.GetBagByUser(id);

            return b;
        }
        //שולף מתנות לפי מתנה
        public async Task<IEnumerable<Bag>> GetBagByGift(int id)
        {
            var b = await _bagRepository.GetBagByGift(id);

            return b;
        }
    }

}

//using Microsoft.EntityFrameworkCore;
//using SaleApi.Models;
//using SaleApi.Repositories;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Text.Json.Serialization;
//using static SaleApi.Dto.BagDto;
//using static SaleApi.Dto.GiftDto;

//namespace SaleApi.Services
//{
//    public class BagService : IBagService
//    {
//        private readonly IBagRepository _bagRepository;
//        //private readonly IGiftRepository _giftRepository;
//        private readonly ILogger<BagService> _logger;

//        public BagService(IBagRepository bagRepository, IGiftRepository giftRepository, ILogger<BagService> logger)
//        {
//            _bagRepository = bagRepository;
//            _logger = logger;
//            //  _giftRepository = giftRepository;
//        }


//        //כל הסלים
//        public async Task<IEnumerable<GetBagDto>> GetAllBag()
//        {
//            var allBags = await _bagRepository.GetAllBag();
//            var bagDto = allBags
//                .Where(b => b.Gift != null)
//    .Select(b => new GetBagDto
//    {
//        Id = b.Id,
//        IdUser = b.IdUser,
//        IdGift = b.IdGift,
//        Gift = new GiftResponseDto
//        {
//            Id=b.Id,
//            Name = b.Gift.Name,
//            Description = b.Gift.Description,
//            Img = b.Gift.Img,
//            Price = b.Gift.Price,
//            IdDoner = b.Gift.IdDoner,
//        }
//    })
//    .ToList();
//            return bagDto;
//        }


//        //סל חדש
//        public async Task<CreateBagDto> NewGiftToBag(CreateBagDto bagDto)
//        {

//            var bag = new Bag
//            {
//                IdUser = bagDto.IdUser,
//                IdGift = bagDto.IdGift,
//                //Gift = gift
//            };
//            var created = await _bagRepository.NewGiftToBag(bag);
//            _logger.LogInformation("Bag created with ID: {BagId}", created.Id);

//            return new CreateBagDto
//            {
//                IdUser = created.IdUser,
//                IdGift = created.IdGift,
//                };
//            }


//        //מחיקה מהסל

//        public async Task DeleteBag(int id)
//        {
//            await _bagRepository.DeleteBag(id);
//        }

//        //GetBagById
//        public async Task<Bag> GetBagById(int id)
//        {
//            var b = await _bagRepository.GetBagById(id);
//            if (b == null) return null;
//            return b;
//        }

//        //שולף מתנות לפי משתמש
//        public async Task<IEnumerable<Bag>> GetBagByUser(int id)
//        {
//            var b = await _bagRepository.GetBagByUser(id);

//            return b;
//        }
//        //שולף מתנות לפי מתנה
//        public async Task<IEnumerable<Bag>> GetBagByGift(int id)
//        {
//            var b = await _bagRepository.GetBagByGift(id);

//            return b;
//        }
//    }
//}
