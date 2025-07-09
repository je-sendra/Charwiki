using Charwiki.ClassLib.Dto.Request;
using Charwiki.ClassLib.Dto.Response;
using Charwiki.WebApi.Extensions;
using Charwiki.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Charwiki.WebApi.Controllers;

/// <summary>
/// Controller for managing tags.
/// Tags can be used to categorize or label Loomian sets for easier identification and organization.
/// </summary>
/// <param name="charwikiDbContext"></param>
[Route("[controller]")]
public class TagsController(CharwikiDbContext charwikiDbContext) : ControllerBase
{
    /// <summary>
    /// Gets all tags.
    /// </summary>
    /// <returns>A list of tags.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllTags()
    {
        IEnumerable<Tag> tags = await charwikiDbContext.Tags.ToListAsync();
        IEnumerable<TagResponseDto> tagDtos = tags.Select(tag => tag.ToResponseDto());
        return Ok(tagDtos);
    }

    /// <summary>
    /// Gets a tag by its ID.
    /// </summary>
    /// <param name="id">The ID of the tag.</param>
    /// <returns>The tag with the specified ID.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTagById(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        Tag? tag = await charwikiDbContext.Tags.FindAsync(id);
        if (tag == null)
        {
            return NotFound();
        }
        TagResponseDto tagDto = tag.ToResponseDto();
        return Ok(tagDto);
    }

    /// <summary>
    /// Creates a new tag.
    /// </summary>
    /// <param name="tagDto">The tag to create.</param>
    /// <returns>The created tag.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin,Moderator")]
    public async Task<IActionResult> CreateTag([FromBody] CreateTagRequestDto tagDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (string.IsNullOrWhiteSpace(tagDto.Name))
        {
            return BadRequest("Tag name cannot be empty.");
        }

        Tag tag = tagDto.ToEntity();

        charwikiDbContext.Tags.Add(tag);
        await charwikiDbContext.SaveChangesAsync();
        TagResponseDto tagResponseDto = tag.ToResponseDto();
        return Created($"/tags/{tag.Id}", tagResponseDto);
    }
}