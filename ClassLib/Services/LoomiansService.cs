using Charwiki.ClassLib.Configuration;
using Charwiki.ClassLib.Models;
using Charwiki.ClassLib.Services.Templates;
using Microsoft.Extensions.Options;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Service for Loomian-related operations against an API.
/// </summary>
/// <param name="httpClient"></param>
/// <param name="apiSettings"></param>
public class LoomiansService(HttpClient httpClient, IOptions<ApiSettings> apiSettings) : CrudControllerServiceTemplate<Loomian>(httpClient, apiSettings, "loomians"), ILoomiansService
{

}