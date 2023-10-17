using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using VewTech.Charwiki.Library.Models;

namespace VewTech.Charwiki.API.Controllers;

/// <summary>
/// The controller for interacting with the Loomian model.
/// </summary>
/// <param name="dataContext">A DataContext for the controller to use.</param>
[Route("[controller]")]
public class LoomianController(DataContext dataContext) : Controller
{
    #region Basic Loomian Operations
    /// <summary>
    /// Gets all the Loomians
    /// </summary>
    /// <returns>A list with all the Loomians.</returns>
    [HttpGet]
    public ActionResult<IEnumerable<Loomian>> Get()
    {
        return dataContext.Loomians;
    }

    /// <summary>
    /// Creates a new Loomian.
    /// </summary>
    /// <param name="loomian">The Loomian to be created.</param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult<Loomian> Post([FromBody] Loomian loomian)
    {
        dataContext.Loomians.Add(loomian);
        dataContext.SaveChanges();
        return Created("", loomian);
    }

    /// <summary>
    /// Get a specific Loomian by its id.
    /// </summary>
    /// <param name="id">The id to search.</param>
    /// <returns>The Loomian with the specified Id.</returns>
    [HttpGet("{id}")]
    public ActionResult<Loomian?> GetById(Guid id)
    {
        var loomian = dataContext.Loomians.Find(id);
        if (loomian == null) return NotFound();
        return Ok(loomian);
    }

    /// <summary>
    /// Updates a Loomian by its id.
    /// </summary>
    /// <param name="id">The id of the Loomian to update.</param>
    /// <param name="loomianPatch">The Loomian patch.</param>
    /// <returns>The updated Loomian.</returns>
    [HttpPatch("{id}")]
    public ActionResult<Loomian> Patch(Guid id, [FromBody] JsonPatchDocument<Loomian> loomianPatch)
    {
        var loomian = dataContext.Loomians.Find(id);
        if (loomian == null) return NotFound();
        loomianPatch.ApplyTo(loomian);
        dataContext.SaveChanges();
        return loomian;
    }
    #endregion

    #region Basic Set Operations
    /// <summary>
    /// Gets all sets for the Loomian with the specified Id.
    /// </summary>
    /// <param name="id">The id of the Loomian.</param>
    /// <returns>A list of all the sets the Loomian has.</returns>
    [HttpGet("{id}/sets")]
    public ActionResult<IEnumerable<Set>?> GetSet(Guid id)
    {
        var loomian = dataContext.Loomians.Find(id);
        if (loomian == null) return NotFound();
        return loomian.Sets;
    }

    /// <summary>
    /// Creates a new set for the Loomian with the specified Id.
    /// </summary>
    /// <param name="id">The id of the Loomian to create a Set for.</param>
    /// <param name="set">The Set to be created.</param>
    /// <returns>The created Set.</returns>
    [HttpPost("{id}/sets")]
    public ActionResult<Set> PostSet(Guid id, [FromBody] Set set)
    {
        var loomian = dataContext.Loomians.Find(id);
        if (loomian == null) return NotFound();
        loomian.Sets.Add(set);
        dataContext.SaveChanges();
        return Created("", set);
    }

    /// <summary>
    /// Gets a set by its id, from all the sets of the Loomian with the specified id.
    /// </summary>
    /// <param name="id">The id of the Loomian to search sets from.</param>
    /// <param name="setId">The id of the Set to be retrieved.</param>
    /// <returns>The Set with the specified id.</returns>
    [HttpGet("{id}/sets/{setId}")]
    public ActionResult<Set> GetSetById(Guid id, Guid setId)
    {
        var loomian = dataContext.Loomians.Find(id);
        if (loomian == null) return NotFound();
        var set = loomian.Sets.Find(currentSet => currentSet.Id == setId);
        if (set == null) return NotFound();
        return Ok(set);
    }

    /// <summary>
    /// Updates a Set by its id.
    /// </summary>
    /// <param name="id">The id of the Loomian that has the Set.</param>
    /// <param name="setId">The id of the Set to update.</param>
    /// <param name="setPatch">The Set patch.</param>
    /// <returns>The updated Set.</returns>
    [HttpPatch("{id}/sets/{setId}")]
    public ActionResult<Set> PatchSet(Guid id, Guid setId, [FromBody] JsonPatchDocument<Set> setPatch)
    {
        var loomian = dataContext.Loomians.Find(id);
        if (loomian == null) return NotFound();
        var set = loomian.Sets.Find(currentSet => currentSet.Id == setId);
        if (set == null) return NotFound();
        setPatch.ApplyTo(set);
        dataContext.SaveChanges();
        return set;
    }
    #endregion
}