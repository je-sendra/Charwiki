using Charwiki.ClassLib.Configuration;
using Charwiki.ClassLib.Models;
using Charwiki.ClassLib.Services.Templates;
using Microsoft.Extensions.Options;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Service for game version info-related operations against an API.
/// </summary>
/// <param name="httpClient"></param>
/// <param name="apiSettings"></param>
public class GameVersionInfosService(HttpClient httpClient, IOptions<ApiSettings> apiSettings) : CrudControllerServiceTemplate<GameVersionInfo>(httpClient, apiSettings, "gameVersionInfos"), IGameVersionInfosService
{
}