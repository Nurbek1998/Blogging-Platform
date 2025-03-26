using Blogging_Platform.Abstractions;
using Blogging_Platform.DTOs.CommentDTOs;
using Blogging_Platform.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blogging_Platform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "AdminAuthorPolicy")]
    public class CommentController(ICommentService _service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _service.GetCommentsAsync(
                User.GetUserId(),
                User.IsInRole("admin"));

            return Ok(comments);
        }

        [HttpGet("{commentId}")]
        public async Task<IActionResult> GetCommentById(Guid commentId)
        {
            var comment = await _service.GetCommentByIdAsync(
                User.GetUserId(),
                commentId,
                User.IsInRole("admin"));

            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(CommentForCreationDto dto)
        {
            var comment = await _service.CreateCommentAsync(
                User.GetUserId(), dto);

            return CreatedAtAction(nameof(GetCommentById), new { commentId = comment.Id }, comment);
        }

        [HttpPut("{commentId}")]
        public async Task<IActionResult> UpdateComment(Guid commentId, CommentForUpdateDto dto)
        {
            var comment = await _service.UpdateCommentAsync(
                User.GetUserId(),
                commentId, dto,
                User.IsInRole("admin"));

            return Ok(comment);
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(Guid commentId)
        {
            var result = await _service.DeleteCommentAsync(
                User.GetUserId(),
                commentId,
                User.IsInRole("admin"));

            if (result is true)
                return Ok("Deleted successfully");

            return BadRequest();
        }
    }
}
