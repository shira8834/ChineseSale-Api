using Microsoft.EntityFrameworkCore;
using SaleApi.Data;
using SaleApi.Models;

namespace SaleApi.Repositories
{
    public class BagRepository : IBagRepository
    {

        SaleContextDB _context;
        public BagRepository(SaleContextDB saleContextDB)
        {
            _context = saleContextDB;
        }


        ///כל הסלים
        public async Task<IEnumerable<Bag>> GetAllBag()
        {
            return await _context.Bags.Include(b=> b.Gift).ToListAsync();
        }

        //סל חדש- הוספה לסל
        public async Task<Bag> NewGiftToBag(Bag bag)
        {
            _context.Bags.Add(bag);
            await _context.SaveChangesAsync();
            return bag;
        }
       
        //// מחיקת כל הסל של משתמש מסוים
        //     public async Task ClearUserBag(int userId)
        //{
        //    var userItems = await _context.Bags.Where(b => b.IdUser == userId).ToListAsync();
        //    if (userItems.Any())
        //    {
        //        _context.Bags.RemoveRange(userItems);
        //        await _context.SaveChangesAsync();
        //    }
        //}
        //מחיקה סל

        public async Task DeleteBag(int id)
        {
            var bag = await _context.Bags.FindAsync(id);
            if (bag != null)
            {
                _context.Bags.Remove(bag);
                await _context.SaveChangesAsync();

            }

        }


        // חיפוש לפי ID
        public async Task<Bag?> GetBagById(int id)
        {
            return await _context.Bags.Include(b=>b.Gift)
                .Include(b=>b.User)
                .FirstOrDefaultAsync(b=>b.Id==id);
       
        }

        //חיפוש לפי ID משתמש
        public async Task<IEnumerable<Bag?>> GetBagByUser(int id)
        {
            var b = await _context.Bags.Where(b => b.IdUser == id).ToListAsync();
            
            return b;
        }

        //חיפוש לפי ID מתנה
        public async Task<IEnumerable<Bag?>> GetBagByGift(int id)
        {
            var b = await _context.Bags.Where(b => b.IdGift == id).ToArrayAsync();
            return b;
        }

        
    }
}
