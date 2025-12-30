using Microsoft.EntityFrameworkCore;
using SaleApi.Data;
using SaleApi.Models;
using static SaleApi.Dto.DonerDto;

namespace SaleApi.Repositories
{
    public class DonerRepository : IDonerRepository
    {
        SaleContextDB _context;
        public DonerRepository(SaleContextDB saleContextDB)
        {
            _context = saleContextDB;
        }


        ///כל התורמים
        public async Task<IEnumerable<Doner>> GetAllDoner()
        {
            return await _context.Doners.ToListAsync();
        }

        //תורם חדש
        public async Task<Doner> NewDoner(Doner doner)
        {
            _context.Doners.Add(doner);
            await _context.SaveChangesAsync();
            return doner;
        }


        //מחיקת תורם
        public async Task DeleteDoner(int id)
        {
            var doner = await _context.Doners.FindAsync(id);
            if (doner != null)
            {
                _context.Doners.Remove(doner);
                await _context.SaveChangesAsync();

            }

        }

        //עידכון תורם
        public async Task<Doner> UpdateDoner(Doner doner)
        {
            _context.Doners.Update(doner);
            await _context.SaveChangesAsync();
            return doner;
        }

        //GetDonerById
        public async Task<Doner?> GetDonerById(int id)
        {
            return await _context.Doners.FindAsync(id);
        }

        //הוספת תרומה- מוצר לתורם
        public async Task AddGiftToDoner(int donerId, Gift gift)
        {
            var doner = await _context.Doners.Include(d => d.Gifts).FirstOrDefaultAsync(d => d.Id == donerId);
            if (doner != null)
            {
                doner.Gifts.Add(gift);
                await _context.SaveChangesAsync();
            }
        }

        //כל התרומות של התורם


    }
}
