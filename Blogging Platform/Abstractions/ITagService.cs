using Blogging_Platform.DTOs.TagDTOs;
using Blogging_Platform.Entities;

namespace Blogging_Platform.Abstractions
{
    public interface ITagService
    {
        public Task<TagResultDto> CreateTagAsync(TagModel dto);
        public Task<bool> DeleteTagAsync(Guid id, bool isAdmin);
        public Task<TagResultDto> UpdateTagAsync(Guid id, TagModel model);
        public Task<IEnumerable<TagResultDto>> GetTagsAsync(Guid userId, bool isAdmin);
        public Task<TagResultDto> GetTagByIdAsync(Guid userId, Guid TagId, bool isAdmin);
    }
}
