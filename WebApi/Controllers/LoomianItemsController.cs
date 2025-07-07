using Charwiki.ClassLib.Dto.Request;
using Charwiki.ClassLib.Dto.Response;
using Charwiki.WebApi.Extensions;
using Charwiki.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Charwiki.WebApi.Controllers;

/// <summary>
/// Controller for Loomian item-related endpoints.
/// </summary>
/// <param name="charwikiDbContext"></param>
[Route("[controller]")]
public class LoomianItemsController(CharwikiDbContext charwikiDbContext) : ControllerBase
{
    /// <summary>
    /// Creates a new Loomian item.
    /// </summary>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateNewAsync([FromBody] CreateLoomianItemRequestDto requestDto)
    {
        // Validate the model
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Check if the item already exists
        if (await charwikiDbContext.LoomianItems.AnyAsync(x => x.Name == requestDto.Name))
        {
            return Conflict($"Loomian item with name '{requestDto.Name}' already exists.");
        }

        // Convert the request DTO to a LoomianItem entity
        LoomianItem loomianItem = requestDto.ToEntity();

        // Add the new item to the database
        await charwikiDbContext.LoomianItems.AddAsync(loomianItem);
        await charwikiDbContext.SaveChangesAsync();

        // Return the created item as a response DTO
        LoomianItemResponseDto responseDto = loomianItem.ToResponseDto();

        return Created($"/loomianItems/{responseDto.Id}", responseDto);
    }

    /// <summary>
    /// Gets all Loomian items.
    /// </summary>
    /// <returns>A collection of Loomian items.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        IEnumerable<LoomianItem> loomianItems = await charwikiDbContext.LoomianItems.ToListAsync();
        IEnumerable<LoomianItemResponseDto> responseDtos = loomianItems
            .Select(loomianItem => loomianItem.ToResponseDto());
        return Ok(responseDtos);
    }
}