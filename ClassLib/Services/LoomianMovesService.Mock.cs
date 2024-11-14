using Charwiki.ClassLib.Models;
using Charwiki.ClassLib.Services.Templates;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Service for Loomian move-related operations with prefabricated mock data.
/// </summary>
public class MockLoomianMovesService() : MockCrudControllerServiceTemplate<LoomianMove>("loomianMoves.json"), ILoomianMovesService
{
}