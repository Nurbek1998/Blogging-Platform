using Blogging_Platform.Abstractions;
using Blogging_Platform.Data;
using Blogging_Platform.DTOs.TagDTOs;
using Blogging_Platform.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blogging_Platform.Services
{
    public class TagService(AppDbContext _context) : ITagService
    {
        public async Task<TagResultDto> CreateTagAsync(TagModel dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            var tag = new Tag
            {
                Name = dto.Name
            };

            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();

            return new TagResultDto { Id = tag.Id, Name = tag.Name };
        }

        public async Task<bool> DeleteTagAsync(Guid id, bool isAdmin)
        {
            var tag = await _context.Tags
                .FirstOrDefaultAsync(x => x.Id == id);

            if (tag is null)
                return false;

            if (!isAdmin)
                throw new UnauthorizedAccessException("You are not authorized to delete this tag.");

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<TagResultDto> GetTagByIdAsync(Guid userId, Guid tagId, bool isAdmin)
        {
            var tagQuery = _context.Tags
           .Where(t => t.Id == tagId)
           .Include(t => t.PostTags)
               .ThenInclude(pt => pt.Post)
           .AsQueryable();

            if (!isAdmin)
            {
                tagQuery = tagQuery.Where(t => t.PostTags.Any(pt => pt.Post.UserId == userId));
            }

            var tag = await tagQuery.FirstOrDefaultAsync();

            if (tag is null)
                return null;

            return new TagResultDto { Id = tag.Id, Name = tag.Name };
        }

        public async Task<IEnumerable<TagResultDto>> GetTagsAsync(Guid userId, bool isAdmin)
        {
            IQueryable<Tag> query = _context.Tags;

            if (isAdmin)
            {
                query = query.Include(x => x.PostTags)
                    .ThenInclude(x => x.Post).AsQueryable();
            }
            else
            {
                query = query.Include(t => t.PostTags.Where(x => x.Post.UserId == userId));
            }

            List<TagResultDto> tags = await query
                .Select(t => new TagResultDto { Id = t.Id, Name = t.Name })
                .ToListAsync();

            return tags;
        }

        public async Task<TagResultDto> UpdateTagAsync(Guid id, TagModel model)
        {
            var tag = await _context.Tags
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tag is null)
                throw new InvalidDataException(nameof(tag));

            tag.Name = model.Name;

            await _context.SaveChangesAsync();

            return new TagResultDto { Id = tag.Id, Name = tag.Name };
        }
    }
}
