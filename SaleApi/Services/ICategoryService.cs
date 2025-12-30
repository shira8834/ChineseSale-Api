using SaleApi.Models;
using static SaleApi.Dto.CategoryDto;

namespace SaleApi.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategory();
        Task<CreateCategoryDto> NewCategory(CreateCategoryDto categoryDto);
        Task DeleteCategory(int id);
        Task<Category> GetCategoryById(int id);
        Task<GetCategoryDto> UpdateCategory(GetCategoryDto CategoryDto);

    }
}