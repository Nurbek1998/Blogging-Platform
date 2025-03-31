using Blogging_Platform.Abstractions;
using Blogging_Platform.DTOs.PostDTOs;
using Blogging_Platform.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Blogging_Platform.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "AdminAuthorPolicy")]
public class PostController(IPostService _service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreatePost(PostModel dto)
    {
        var post = await _service.CreatePostAsync(User.GetUserId(), dto);

        return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPostById(Guid id)
    {
        var post = await _service.GetPostByIdAsync(User.GetUserId(), id, User.IsInRole("admin"));

        return Ok(post);
    }

    [HttpGet]
    public async Task<IActionResult> GetPosts()
    {
        var posts = await _service.GetPostsAsync(User.GetUserId(), User.IsInRole("admin"));
        
        return Ok(posts);
    }

    [HttpPut("{postId}")]
    public async Task<IActionResult> UpdatePost(Guid postId, PostForUpdateDto postModel)
    {
        var post = await _service.UpdatePostAsync(User.GetUserId(), postId, postModel);
        
        return Ok(post);
    }

    [HttpDelete("{postId}")]
    public async Task<IActionResult> DeletePost(Guid postId)
    {
        var result = await _service.DeletePostAsync(User.GetUserId(), postId, User.IsInRole("admin"));

        if (result is false)
            return BadRequest();

        return Ok("Deleted successfully!");
    }
}
