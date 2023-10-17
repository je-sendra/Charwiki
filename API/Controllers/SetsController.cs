using Microsoft.AspNetCore.Mvc;
using VewTech.Charwiki.Library.Helpers;
using VewTech.Charwiki.Library.Models;

namespace VewTech.Charwiki.API.Controllers;

/// <summary>
/// The controller for interacting with the Set model.
/// </summary>
/// <param name="dataContext">A DataContext for the controller to use.</param>
[Route("[controller]")]
public class SetsController(DataContext dataContext) : ApiController<Set>(dataContext, dataContext.Sets);