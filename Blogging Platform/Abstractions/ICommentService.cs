using Blogging_Platform.DTOs.CommentDTOs;
using Blogging_Platform.Entities;

namespace Blogging_Platform.Abstractions
{
    public interface ICommentService
    {
        public Task<IEnumerable<Comment>> GetCommentsAsync(Guid userId, bool isAdmin, int pageNumber = 1, int pageSize = 10);
        public Task<Comment> GetCommentByIdAsync(Guid userId, Guid commentId, bool isAdmin);
        public Task<Comment> CreateCommentAsync(Guid userId, CommentForCreationDto model);
        public Task<Comment> UpdateCommentAsync(Guid userId, Guid commentId, CommentForUpdateDto model, bool isAdmin);
        public Task<bool> DeleteCommentAsync(Guid userId, Guid CommentId, bool isAdmin);
    }
}
