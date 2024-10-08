using Charwiki.ClassLib.Configuration;
using Charwiki.ClassLib.Models;
using Charwiki.ClassLib.Services.Templates;
using Microsoft.Extensions.Options;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Service for Loomian ability-related operations against an API.
/// </summary>
/// <param name="httpClient"></param>
/// <param name="apiSettings"></param>
public class LoomianAbilitiesService(HttpClient httpClient, IOptions<ApiSettings> apiSettings) : CrudControllerServiceTemplate<LoomianAbility>(httpClient, apiSettings, "abilities"), ILoomianAbilitiesService
{
}