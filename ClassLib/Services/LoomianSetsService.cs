using Charwiki.ClassLib.Configuration;
using Charwiki.ClassLib.Models;
using Charwiki.ClassLib.Services.Templates;
using Microsoft.Extensions.Options;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Service for Loomian set-related operations against an API.
/// </summary>
/// <param name="httpClient"></param>
/// <param name="apiSettings"></param>
public class LoomianSetsService(HttpClient httpClient, IOptions<ApiSettings> apiSettings) : CrudControllerServiceTemplate<LoomianSet>(httpClient, apiSettings, "loomianSets"), ILoomianSetsService
{
}