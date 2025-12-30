using SaleApi.Dto;
using SaleApi.Models;
using SaleApi.Repositories;
using static SaleApi.Dto.DonerDto;

namespace SaleApi.Services
{
    public class DonerService : IDonerService
    {

        private readonly IDonerRepository _donerRepository;
        public DonerService(IDonerRepository donerRepository)
        {
            _donerRepository = donerRepository;
        }

        public async Task<IEnumerable<Doner>> GetAllDoner()
        {
            var doners = await _donerRepository.GetAllDoner();
            return doners ?? Enumerable.Empty<Doner>();
        }

        ///תורם חדש
        public async Task<CreateDonerDto> NewDoner(CreateDonerDto donerDto)
        {
            var doner = new Doner
            {
                FirstName = donerDto.FirstName,
                LastName = donerDto.LastName,
                Email = donerDto.EMail
            };
            var cerated = await _donerRepository.NewDoner(doner);
            return new CreateDonerDto
            {
                FirstName = cerated.FirstName,
                LastName = cerated.LastName,
                EMail = cerated.Email
            };
        }

        //מחיקת תורם
        public async Task DeleteDoner(int id)
        {
            await _donerRepository.DeleteDoner(id);
        }



        //GetDonerById
        public async Task<Doner> GetDonerById(int id)
        {
            var d = await _donerRepository.GetDonerById(id);
            if (d == null) return null;
            return d;
        }




        //עידכון תורם
        public async Task<UpdateDonerDto> UpdateDoner(UpdateDonerDto donerDto)
        {
            var existing = await _donerRepository.GetDonerById(donerDto.Id);
            if (existing == null) return null;

            existing.FirstName = donerDto.FirstName ?? existing.FirstName;
            existing.LastName = donerDto.LastName ?? existing.LastName;
            existing.Email = donerDto.EMail ?? existing.Email;

            var updatedDoner = await _donerRepository.UpdateDoner(existing);
            if (updatedDoner == null) return null;
            return new UpdateDonerDto
            { Id = updatedDoner.Id, FirstName = updatedDoner.FirstName, LastName = updatedDoner.LastName, EMail = updatedDoner.Email };

        }







    }
}