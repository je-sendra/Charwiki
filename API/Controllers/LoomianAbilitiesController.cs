using Microsoft.AspNetCore.Mvc;
using VewTech.Charwiki.Library.Helpers;
using VewTech.Charwiki.Library.Models;

namespace VewTech.Charwiki.API.Controllers;

/// <summary>
/// The controller for interacting with the LoomianAbility model.
/// </summary>
/// <param name="dataContext">A DataContext for the controller to use.</param>
[Route("[controller]")]
public class LoomianAbilitiesController(DataContext dataContext) : ApiController<LoomianAbility>(dataContext, dataContext.LoomianAbilities);