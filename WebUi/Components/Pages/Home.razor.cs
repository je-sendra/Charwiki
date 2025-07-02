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

    private Guid? SelectedLoomianId { get; set; }
    private Guid? SelectedAbilityId { get; set; }
    private Guid? SelectedItemId { get; set; }
    private Guid? SelectedMoveId { get; set; }
    private Guid? SelectedGameVersionInfoId { get; set; }
    private Guid? SelectedTagId { get; set; }

    /// <inheritdoc/>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            SelectedLoomianId = null;
            SelectedAbilityId = null;
            SelectedItemId = null;
            SelectedMoveId = null;
            SelectedGameVersionInfoId = null;
            SelectedTagId = null;

            await UpdateLoomianSetsAsync();

            StateHasChanged();
        }
    }

    private async Task UpdateLoomianSetsAsync()
    {
        LoomianSetQueryParams queryParams = new()
        {
            HideNonApprovedSets = false,
            IncludeMetadata = true,
            IncludeLoomian = true,
            IncludeAverageRating = true,
            IncludeTags = true,
            LoomianId = SelectedLoomianId,
            AbilityId = SelectedAbilityId,
            ItemId = SelectedItemId,
            MoveId = SelectedMoveId,
            GameVersionInfoId = SelectedGameVersionInfoId,
        };
        if (SelectedTagId != null)
        {
            queryParams.TagsIds = [SelectedTagId.Value];
        }

        IEnumerable<LoomianSetResponseDto>? loomianSetsFromDb = await LoomianSetsService.GetAllAsync(queryParams);
        if (loomianSetsFromDb != null)
        {
            LoomianSets = loomianSetsFromDb;
        }

        StateHasChanged();
    }
}