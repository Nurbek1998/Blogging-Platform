using Blogging_Platform.Abstractions;
using Blogging_Platform.Data;
using Blogging_Platform.DTOs.CategoryDTOs;
using Blogging_Platform.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blogging_Platform.Services;
public class CategoryService(AppDbContext _context) : ICategoryService
{
    public async Task<Category> CreateCategoryAsync(CategoryModel categoryModel)
    {
        if (categoryModel is null)
            throw new ArgumentNullException(nameof(categoryModel));

        var category = new Category()
        {
            Name = categoryModel.Name
        };

        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();

        return category;
    }

    public async Task<bool> DeleteCategoryAsync(Guid id)
    {
        var category = await _context
            .Categories
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category is null)
            return false;

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync(Guid userId, bool isAdmin)
    {
        IQueryable<Category> query = _context.Categories;

        if (isAdmin)
        {
            query = query.Include(p => p.Posts)
                .ThenInclude(x => x.Comments);
        }
        else
        {
            query = query.Include(x => x.Posts.Where(p => p.UserId == userId))
                .ThenInclude(x => x.Comments); ;
        }

        return await query.ToListAsync();
    }

    public async Task<Category> GetCategoryByIdAsync(Guid categoryId, Guid userId, bool isAdmin)
    {

        var query = _context.Categories
                       .Include(c => c.Posts)
                       .AsQueryable();

        if (!isAdmin)
        {
            query = query.Where(c => c.Posts.Any(p => p.UserId == userId));
        }

        return await query.FirstOrDefaultAsync(c => c.Id == categoryId);
    }

    public async Task<Category> UpdateCategoryAsync(CategoryForUpdateDto dto)
    {
        var existingCategory = await _context.Categories
             .FirstOrDefaultAsync(x => x.Id == dto.Id);

        if (existingCategory is null)
            throw new ArgumentNullException(nameof(existingCategory));

        existingCategory.Name = dto.Name;

        await _context.SaveChangesAsync();

        return existingCategory;
    }
}
