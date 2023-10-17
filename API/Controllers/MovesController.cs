using Microsoft.AspNetCore.Mvc;
using VewTech.Charwiki.Library.Helpers;
using VewTech.Charwiki.Library.Models;

namespace VewTech.Charwiki.API.Controllers;

/// <summary>
/// The controller for interacting with the Move model.
/// </summary>
/// <param name="dataContext">A DataContext for the controller to use.</param>
[Route("[controller]")]
public class MovesController(DataContext dataContext) : ApiController<Move>(dataContext, dataContext.Moves);