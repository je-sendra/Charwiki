using Charwiki.ClassLib.Models;
using Charwiki.ClassLib.Services.Templates;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Service for Loomian items-related operations with prefabricated mock data.
/// </summary>
public class MockLoomianItemsService() : MockCrudControllerServiceTemplate<LoomianItem>("loomianItems.json"), ILoomianItemsService
{
}