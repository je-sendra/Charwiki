using Charwiki.ClassLib.Dto.QueryParams;
using Charwiki.ClassLib.Dto.Response;
using Charwiki.ClassLib.Services;
using Microsoft.AspNetCore.Components;

namespace Charwiki.WebUi.Components.Pages;

/// <summary>
/// Represents a page component for displaying a Loomian set.
/// </summary>
public partial class LoomianSetDisplayPage
{
    /// <summary>
    /// Represents the ID of the Loomian set to display.
    /// This parameter is required and must be provided when using the component.
    /// </summary>
    [Parameter]
    public required Guid SetId { get; set; }

    [Inject]
    private ILoomianSetsService LoomianSetsService { get; set; } = default!;

    private LoomianSetResponseDto? _loomianSet { get; set; }

    /// <inheritdoc/>
    override protected async Task OnParametersSetAsync()
    {
        LoomianSetQueryParams queryParams = new()
        {
            IncludeValueToStatAssignments = true,
            IncludeAverageRating = true,
            IncludeLoomian = true,
            IncludeAbility = true,
            IncludeItem = true,
            IncludeMoves = true,
            IncludeDetailedExplanation = true,
            IncludeMetadata = true,
        };
        _loomianSet = await LoomianSetsService.GetByIdAsync(SetId, queryParams);
    }
}