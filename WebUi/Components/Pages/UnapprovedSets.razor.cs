using Charwiki.ClassLib.Dto.QueryParams;
using Charwiki.ClassLib.Dto.Response;
using Charwiki.ClassLib.Services;
using Microsoft.AspNetCore.Components;

namespace Charwiki.WebUi.Components.Pages;

/// <summary>
/// Represents the page that displays unapproved Loomian sets.
/// </summary>
public partial class UnapprovedSets
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

    private int CurrentPage { get; set; }

    private bool HasMoreSets;

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
            HasMoreSets = true;

            await UpdateLoomianSetsAsync();

            StateHasChanged();
        }
    }

    private async Task UpdateLoomianSetsAsync()
    {
        IEnumerable<LoomianSetResponseDto>? loomianSetsFromDb = await RetrieveSetsFromDb();
        if (loomianSetsFromDb != null)
        {
            LoomianSets = loomianSetsFromDb;
        }

        StateHasChanged();
    }

    private async Task LoadMoreSets()
    {
        CurrentPage++;
        IEnumerable<LoomianSetResponseDto>? moreSets = await RetrieveSetsFromDb();
        if (moreSets != null && moreSets.Any())
        {
            LoomianSets = [..LoomianSets, ..moreSets];
        }
        else
        {
            HasMoreSets = false;
        }
        StateHasChanged();
    }
    
    private async Task<IEnumerable<LoomianSetResponseDto>?> RetrieveSetsFromDb()
    {
        LoomianSetQueryParams queryParams = new()
        {
            HideNonApprovedSets = false,
            HideApprovedSets = true,
            IncludeMetadata = true,
            IncludeLoomian = true,
            IncludeAverageRating = true,
            IncludeTags = true,
            LoomianId = SelectedLoomianId,
            AbilityId = SelectedAbilityId,
            ItemId = SelectedItemId,
            MoveId = SelectedMoveId,
            GameVersionInfoId = SelectedGameVersionInfoId,
            PageSize = 18,
            PageNumber = CurrentPage
        };
        if (SelectedTagId != null)
        {
            queryParams.TagsIds = [SelectedTagId.Value];
        }

        IEnumerable<LoomianSetResponseDto>? loomianSetsFromDb = await LoomianSetsService.GetAllAsync(queryParams);

        StateHasChanged();
        return loomianSetsFromDb;
    }
}