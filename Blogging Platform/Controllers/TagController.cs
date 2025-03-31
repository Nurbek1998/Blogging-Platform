using Blogging_Platform.Abstractions;
using Blogging_Platform.DTOs.TagDTOs;
using Blogging_Platform.Entities;
using Blogging_Platform.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blogging_Platform.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "AdminAuthorPolicy")]
public class TagController(ITagService _service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateTag(TagModel dto)
    {
        var tag = await _service.CreateTagAsync(dto);

        if (tag is null)
            return BadRequest();

        return CreatedAtAction("GetTagById", new { id = tag.Id }, tag);
    }

    [HttpGet("{id}", Name = "GetTagById")]
    public async Task<IActionResult> GetTagById(Guid id)
    {
        var tag = await _service
            .GetTagByIdAsync(User.GetUserId(), id, User.IsInRole("admin"));

        if (tag is null)
            return BadRequest(nameof(tag));

        return Ok(tag);
    }

    [HttpGet]
    public async Task<IActionResult> GetTags()
    {
        var tags = await _service
            .GetTagsAsync(User.GetUserId(), User.IsInRole("admin"));

        return Ok(tags);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateTag(Guid id, TagModel model)
    {
        var tag = await _service.UpdateTagAsync(id, model);
        return Ok(tag);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteTag(Guid id)
    {
        var result = await _service.DeleteTagAsync(id, User.IsInRole("admin"));

        if (!result)
            return NotFound();

        return Ok("Successdully deleted!");

    }
}
