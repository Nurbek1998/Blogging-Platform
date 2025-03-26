using Blogging_Platform.DTOs.CategoryDTOs;
using Blogging_Platform.Entities;

namespace Blogging_Platform.Abstractions
{
    public interface ICategoryService
    {
        public Task<IEnumerable<Category>> GetCategoriesAsync(Guid userId, bool isAdmin);
        public Task<Category> GetCategoryByIdAsync(Guid categoryId, Guid userId, bool isAdmin);
        public Task<Category> CreateCategoryAsync(CategoryModel dto);
        public Task<Category> UpdateCategoryAsync(CategoryForUpdateDto dto);
        public Task<bool> DeleteCategoryAsync(Guid id);
    }
}
