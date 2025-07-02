using Charwiki.ClassLib.Dto.QueryParams;
using Charwiki.ClassLib.Dto.Response;
using Charwiki.ClassLib.Services;
using Microsoft.AspNetCore.Components;

namespace Charwiki.WebUi.Components.Pages;

/// <summary>
/// Represents the home page component that displays Loomian sets.
/// </summary>
public partial class Home
{
    /// <summary>
    /// Represents the service for Loomian set-related operations.
    /// </summary>
    [Inject]
    public ILoomianSetsService LoomianSetsService { get; set; } = null!;

    private IEnumerable<LoomianSetResponseDto> LoomianSets { get; set; } = [];

    /// <inheritdoc/>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            LoomianSetQueryParams queryParams = new()
            {
                HideNonApprovedSets = true,
                IncludeMetadata = true,
                IncludeLoomian = true,
                IncludeAverageRating = true,
            };
            IEnumerable<LoomianSetResponseDto>? loomianSetsFromDb = await LoomianSetsService.GetAllAsync(queryParams);
            if (loomianSetsFromDb != null)
            {
                LoomianSets = loomianSetsFromDb;
            }
            StateHasChanged();
        }
    }
}