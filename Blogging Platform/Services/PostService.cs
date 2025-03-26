using Blogging_Platform.Abstractions;
using Blogging_Platform.Data;
using Blogging_Platform.DTOs.PostDTOs;
using Blogging_Platform.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blogging_Platform.Services
{
    public class PostService(AppDbContext _context) : IPostService
    {
        public async Task<Post> CreatePostAsync(Guid userId, PostModel dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            var newId = Guid.NewGuid();

            dto.TagIds ??= [];

            var post = new Post
            {
                Id = newId,
                Title = dto.Title,
                Content = dto.Content,
                CreatedAt = DateTime.UtcNow,
                Status = dto.Status,
                UserId = userId,
                CategoryId = dto.CategoryId,
                PostTags = dto.TagIds.Select(t => new PostTag
                {
                    PostId = newId,
                    TagId = t
                }).ToList()
            };

            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();

            return post;
        }

        public async Task<bool> DeletePostAsync(Guid userId, Guid postId, bool isAdmin)
        {
            var post = await _context.Posts
                 .FirstOrDefaultAsync(p => p.Id == postId);

            if (post is null)
                return false;

            if (!isAdmin && post.UserId != userId)
                throw new UnauthorizedAccessException("You are not authorized to delete this post.");

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Post> GetPostByIdAsync(Guid userId, Guid postId, bool isAdmin)
        {
            var query = _context.Posts
                .Where(p => p.Id == postId)
                .Include(x => x.PostTags)
                    .ThenInclude(pt => pt.Tag)
                .AsQueryable();

            if (!isAdmin)
            {
                query = query.Where(p => p.UserId == userId);
            }

            var post = await query.FirstOrDefaultAsync();

            if (post == null)
                throw new InvalidOperationException("Post not found.");

            return post;
        }

        public async Task<IEnumerable<PostForResultDto>> GetPostsAsync(Guid userId, bool isAdmin)
        {
            IQueryable<Post> query = _context.Posts
                .Include(x => x.PostTags)
                .Include(x => x.Comments);


            if (!isAdmin)
            {
                query = query.Where(p => p.PostTags.Any(x => x.Post.UserId == userId));
            }

            var posts = await query.ToListAsync();
            var postsForResult = new List<PostForResultDto>();

            foreach (var post in posts)
            {
                postsForResult.Add(new PostForResultDto
                {
                    Id = post.Id,
                    Title = post.Title,
                    Content = post.Content,
                    Status = post.Status,
                    CategoryId = post.CategoryId,
                    CreatedAt = post.CreatedAt,
                    PublishedAt = post.PublishedAt,
                    UserId = userId,
                    TagIds = post.PostTags.Select(t => t.TagId).ToList(),
                    CommentIds = post.Comments.Select(x => x.Id).ToList()
                });
            }


            return postsForResult;
        }

        public async Task<Post> UpdatePostAsync(Guid userId, Guid postId, PostForUpdateDto model)
        {
            var existedPost = await _context.Posts
                .Include(p => p.PostTags)
                .ThenInclude(t => t.Tag)
                .FirstOrDefaultAsync(p => p.UserId == userId && p.Id == postId);

            if (existedPost is null)
                throw new ArgumentNullException(nameof(existedPost));

            existedPost.Title = model.Title;
            existedPost.Content = model.Content;
            existedPost.CategoryId = model.CategoryId;

            var oldStatus = existedPost.Status;
            existedPost.Status = model.Status;

            if (oldStatus != "Published" && model.Status == "Published")
            {
                existedPost.PublishedAt = DateTime.UtcNow;
            }

            if (model.TagIds is not null && model.TagIds.Count != 0)
            {
                existedPost.PostTags.Clear();

                foreach (var tagId in model.TagIds)
                {
                    existedPost.PostTags.Add(new PostTag
                    {
                        PostId = existedPost.Id,
                        TagId = tagId
                    });
                }
            }

            await _context.SaveChangesAsync();

            return existedPost;

        }
    }
}
