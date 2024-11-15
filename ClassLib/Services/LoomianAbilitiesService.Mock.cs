using Charwiki.ClassLib.Models;
using Charwiki.ClassLib.Services.Templates;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Service for Loomian ability-related operations with prefabricated mock data.
/// </summary>
public class MockLoomianAbilityService() : MockCrudControllerServiceTemplate<LoomianAbility>("loomianAbilities.json"), ILoomianAbilitiesService
{
}