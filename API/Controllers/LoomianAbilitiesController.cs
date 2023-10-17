using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using VewTech.Charwiki.Library.Models;

namespace VewTech.Charwiki.API.Controllers;

/// <summary>
/// The controller for interacting with the LoomianAbility model.
/// </summary>
/// <param name="dataContext">A DataContext for the controller to use.</param>
[Route("[controller]")]
public class LoomianAbilitiesController(DataContext dataContext) : Controller
{
    /// <summary>
    /// Gets all the Loomian abilities.
    /// </summary>
    /// <returns>A list with all the abilities.</returns>
    [HttpGet]
    public ActionResult<IEnumerable<LoomianAbility>> Get()
    {
        return dataContext.LoomianAbilities;
    }

    /// <summary>
    /// Creates a new loomian ability.
    /// </summary>
    /// <param name="loomianAbility">The loomian ability to be created.</param>
    /// <returns>The newly created LoomianAbility.</returns>
    [HttpPost]
    public ActionResult<LoomianAbility> Post([FromBody] LoomianAbility loomianAbility)
    {
        dataContext.Add(loomianAbility);
        dataContext.SaveChanges();
        return loomianAbility;
    }

    /// <summary>
    /// Gets an ability by its specified id.
    /// </summary>
    /// <param name="id">The id of the ability to be retrieved.</param>
    /// <returns>The ability with the specified id.</returns>
    [HttpGet("{id}")]
    public ActionResult<LoomianAbility> GetById(Guid id)
    {
        var ability = dataContext.LoomianAbilities.Find(id);
        if (ability == null) return NotFound();
        return ability;
    }

    /// <summary>
    /// Updates a LoomianAbility by its id.
    /// </summary>
    /// <param name="id">The id of the LoomianAbility to update.</param>
    /// <param name="abilityPatch">The LoomianAbility patch.</param>
    /// <returns>The updated LoomianAbility.</returns>
    [HttpPatch("{id}")]
    public ActionResult<LoomianAbility> Patch(Guid id, [FromBody] JsonPatchDocument<LoomianAbility> abilityPatch)
    {
        var ability = dataContext.LoomianAbilities.Find(id);
        if (ability == null) return NotFound();
        abilityPatch.ApplyTo(ability);
        dataContext.SaveChanges();
        return ability;
    }
}