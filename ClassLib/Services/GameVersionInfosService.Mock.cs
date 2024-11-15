using Charwiki.ClassLib.Models;
using Charwiki.ClassLib.Services.Templates;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Service for game version info-related operations with prefabricated mock data.
/// </summary>
public class MockGameVersionInfosService() : MockCrudControllerServiceTemplate<GameVersionInfo>("gameVersionInfos.json"), IGameVersionInfosService
{
}