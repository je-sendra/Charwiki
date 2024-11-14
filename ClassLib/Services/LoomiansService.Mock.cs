using Charwiki.ClassLib.Enums;
using Charwiki.ClassLib.Models;
using Charwiki.ClassLib.Services.Templates;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Service for Loomian-related operations with prefabricated mock data.
/// </summary>
public class MockLoomiansService() : MockCrudControllerServiceTemplate<Loomian>("loomians.json"), ILoomiansService 
{
}