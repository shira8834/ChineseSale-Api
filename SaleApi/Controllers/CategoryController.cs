using Microsoft.AspNetCore.Mvc;
using SaleApi.Models;
using SaleApi.Services;
using static SaleApi.Dto.CategoryDto;
using static SaleApi.Dto.DonerDto;

namespace SaleApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService cotegoryService)
        {
            _categoryService = cotegoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategory()
        {
            var category = await _categoryService.GetAllCategory();
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> NewCategory([FromBody] CreateCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _categoryService.NewCategory(dto);
                if (created == null)
                    return BadRequest("Failed to create category.");

                return Ok(created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        //מחיקת קטגוריה
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoner(int id)
        {
            try
            {
                await _categoryService.DeleteCategory(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }



        //get by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            try
            {
                var category = await _categoryService.GetCategoryById(id);
                if (category == null)
                    return NotFound();
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        //עידכון קטגוריה 
        [HttpPut]
        public async Task<ActionResult<Category>> UpdateCategory([FromBody] GetCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var updated = await _categoryService.UpdateCategory(dto);
                if (updated == null)
                    return BadRequest("Failed to update category.");
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
