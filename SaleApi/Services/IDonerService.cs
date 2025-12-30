using SaleApi.Dto;
using SaleApi.Models;

namespace SaleApi.Services
{
    public interface IDonerService
    {
        Task DeleteDoner(int id);
        Task<IEnumerable<Doner>> GetAllDoner();
        Task<Doner> GetDonerById(int id);
        Task<DonerDto.CreateDonerDto> NewDoner(DonerDto.CreateDonerDto donerDto);
        Task<DonerDto.UpdateDonerDto> UpdateDoner(DonerDto.UpdateDonerDto donerDto);
    }
}