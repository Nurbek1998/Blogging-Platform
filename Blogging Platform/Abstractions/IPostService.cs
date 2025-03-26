using Blogging_Platform.DTOs.PostDTOs;
using Blogging_Platform.Entities;

namespace Blogging_Platform.Abstractions
{
    public interface IPostService
    {
        public Task<Post> CreatePostAsync(Guid userId, PostModel dto);
        public Task<IEnumerable<PostForResultDto>> GetPostsAsync(Guid userId, bool isAdmin);
        public Task<bool> DeletePostAsync(Guid userId, Guid postId, bool isAdmin);
        public Task<Post> GetPostByIdAsync(Guid userId, Guid postId, bool isAdmin);
        public Task<Post> UpdatePostAsync(Guid userId, Guid postId, PostForUpdateDto model);
    }
}
