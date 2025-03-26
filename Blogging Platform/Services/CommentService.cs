using Blogging_Platform.Abstractions;
using Blogging_Platform.Data;
using Blogging_Platform.DTOs.CommentDTOs;
using Blogging_Platform.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blogging_Platform.Services
{
    public class CommentService(AppDbContext _context) : ICommentService
    {
        public async Task<Comment> CreateCommentAsync(Guid userId, CommentForCreationDto model)
        {
            var comment = new Comment
            {
                Content = model.Content,
                UserId = userId,
                PostId = model.PostId,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task<bool> DeleteCommentAsync(Guid userId, Guid CommentId, bool isAdmin)
        {
            var existedComment = await _context.Comments
                 .FirstOrDefaultAsync(c => c.Id == CommentId);

            if (existedComment == null)
                throw new KeyNotFoundException("The comment with the specified ID does not exist.");

            if (isAdmin || existedComment.UserId == userId)
            {
                _context.Comments.Remove(existedComment);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<Comment> GetCommentByIdAsync(Guid userId, Guid commentId, bool isAdmin)
        {
            var comment = await _context.Comments
                .FirstOrDefaultAsync(x => x.Id == commentId);

            if (comment == null)
                throw new ArgumentNullException(nameof(comment));

            if (isAdmin || comment.UserId == userId)
                return comment;

            throw new UnauthorizedAccessException("You are not allowed to get this comment");
        }

        public async Task<IEnumerable<Comment>> GetCommentsAsync(Guid userId, bool isAdmin, int pageNumber = 1, int pageSize = 10)
        {
            var query = _context.Comments.AsQueryable();

            if (!isAdmin)
            {
                query = query.Where(x => x.UserId == userId);
            }

            var comments = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return comments;
        }

        public async Task<Comment> UpdateCommentAsync(Guid userId, Guid commentId, CommentForUpdateDto model, bool isAdmin)
        {
            var comment = await _context.Comments
                .FirstOrDefaultAsync(c => c.Id == commentId);

            if (comment is null)
                throw new KeyNotFoundException("The comment with the given ID was not found");

            if (isAdmin || comment.UserId == userId)
            {
                comment.Content = model.Content;
            }

            await _context.SaveChangesAsync();

            return comment;
        }
    }
}
