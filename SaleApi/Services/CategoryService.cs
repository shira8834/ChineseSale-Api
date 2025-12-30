using SaleApi.Models;
using SaleApi.Repositories;
using static SaleApi.Dto.CategoryDto;
using static SaleApi.Dto.DonerDto;

namespace SaleApi.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly ICategoryRepository _categoryrRepository;
        public CategoryService(ICategoryRepository categoryrRepository)
        {
            _categoryrRepository = categoryrRepository;
        }

        public async Task<IEnumerable<Category>> GetAllCategory()
        {
            var category = await _categoryrRepository.GetAllCategory();
            return category ?? Enumerable.Empty<Category>();
        }



        ///קטגוריה חדשה 
        public async Task<CreateCategoryDto> NewCategory(CreateCategoryDto categoryDto)
        {
            var category = new Category
            {
               Name = categoryDto.Name,
               Color = categoryDto.Color,
            };
            var cerated = await _categoryrRepository.NewCategory(category);
            return new CreateCategoryDto
            {
                Name = cerated.Name,
                Color = cerated.Color,
            };
        }


        //מחיקת קטגוריה
        public async Task DeleteCategory(int id)
        {
            await _categoryrRepository.DeleteCategory(id);
        }

        //GetCategoryById
        public async Task<Category> GetCategoryById(int id)
        {
            var c = await _categoryrRepository.GetCategoryById(id);
            if (c == null) return null;
            return c;
        }


        //עידכון קטגוריה
        public async Task<GetCategoryDto> UpdateCategory(GetCategoryDto CategoryDto)
        {
            var existing = await _categoryrRepository.GetCategoryById(CategoryDto.Id);
            if (existing == null) return null;

            existing.Name = CategoryDto.Name?? existing.Name;
            existing.Color=CategoryDto.Color ?? existing.Color;
        
            var updatedCategory = await _categoryrRepository.UpdateCategory(existing);
            if (updatedCategory == null) return null;
            return new GetCategoryDto
            {Name = updatedCategory.Name, Color=updatedCategory.Color };

        }

    }
}
