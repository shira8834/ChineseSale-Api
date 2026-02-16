using Microsoft.EntityFrameworkCore;
using SaleApi.Models;
using SaleApi.Repositories;

namespace SaleApi.Services
{
    public class RandomService : IRandomService
    {
        private readonly IRandomRepository _randomRepo;


        public RandomService(IRandomRepository randomRepo)
        {
            _randomRepo = randomRepo;
        }


        public async Task<int?> PickWinner(int giftId)
        {
            var ticketIds = await _randomRepo.GetOrdersForGift(giftId);

            if (ticketIds == null || !ticketIds.Any())
            {
                return null;
            }

            Random random = new Random();

            var list = ticketIds.ToList();
            int randomIndex = random.Next(list.Count);
            return list[randomIndex];
        }

        public async Task<Winner?> ExecuteDraw(int giftId)
        {
            if (await _randomRepo.IsGiftRandom(giftId))
            {
                throw new Exception("הגרלה עבור מתנה זו כבר בוצעה בעבר.");
            }

            int? winOrderId = await PickWinner(giftId);
            if (winOrderId == null)
            {
                throw new KeyNotFoundException("לא ניתן לבצע הגרלה: אין אף משתתף שרכש כרטיס למתנה זו.");
            }


            if (winOrderId == null)
            {
                return null;
            }

            var winOrder = await _randomRepo.GetOrderById(winOrderId.Value);

            var winner = new Winner
            {
                IdGift = giftId,
                IdUser = winOrder.IdUser
            };
            await _randomRepo.SaveWinner(winner, winOrder.Id);

            return winner;
        }
    }
}
