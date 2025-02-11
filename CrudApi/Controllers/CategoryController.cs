using CrudApi.Dtos;
using CrudApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrudApi.Controllers
{
    [Route("category/")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService categoryService;

        public CategoryController(CategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpPost("add_category")]
        public IActionResult AddCategory([FromBody] CategoryCreateDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var category = categoryService.AddCategory(categoryDto);
                return Ok(category);
            }
            catch (ArgumentException ex)
            {
                // Return bad request with the custom error message
                return Ok(ex.Message);
            }
            catch (Exception ex)
            {
                // Handle unexpected server errors
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("get_all_category")]
        public IActionResult GetAllCategory()
        {
            try
            {
                var category = categoryService.GetAllCategory();
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("get_category/{code}")]
        public IActionResult GetCategory(string code)
        {
            try
            {
                var category = categoryService.GetCategoryByCode(code);
                return Ok(category);
            }
            catch (KeyNotFoundException ex)
            {
                return Ok(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("update_category/{code}")]
        public IActionResult UpdateCategory(string code, [FromBody] CategoryUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedCategory = categoryService.UpdateCategory(code, updateDto);
                return Ok(updatedCategory);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("delete_category/{code}")]
        public IActionResult DeleteCategory(string code)
        {
            try
            {
                categoryService.DeleteCategory(code);
                return Ok("Category has been deleted.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
