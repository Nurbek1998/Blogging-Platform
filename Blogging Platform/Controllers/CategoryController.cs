using Blogging_Platform.Abstractions;
using Blogging_Platform.DTOs.CategoryDTOs;
using Blogging_Platform.Entities;
using Blogging_Platform.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blogging_Platform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "AdminAuthorPolicy")]
    public class CategoryController(ICategoryService _service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _service
                .GetCategoriesAsync(User.GetUserId(), User.IsInRole("admin"));

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            var category = await _service
                .GetCategoryByIdAsync(id, User.GetUserId(), User.IsInRole("admin"));

            if (category is null)
                return NotFound();

            return Ok(category);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var deleted = await _service.DeleteCategoryAsync(id);

            if (deleted is false)
                return NotFound();

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateCategory(CategoryModel category)
        {
            var result = await _service.CreateCategoryAsync(category);

            if (result is null)
                return BadRequest();

            return CreatedAtAction(nameof(GetCategory), new { id = result.Id }, result);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateCategory(CategoryForUpdateDto category)
        {
            var result = await _service.UpdateCategoryAsync(category);

            if (result is null)
                return BadRequest();

            return Ok(result);
        }
    }
}
