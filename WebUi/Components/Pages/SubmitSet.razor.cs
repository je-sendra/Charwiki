using Charwiki.ClassLib.Dto.Request;
using Charwiki.ClassLib.Dto.Response;
using Charwiki.ClassLib.Extensions;
using Charwiki.ClassLib.Models;
using Charwiki.ClassLib.Models.OperationResult;
using Charwiki.ClassLib.Services;
using Charwiki.WebUi.Services;
using Microsoft.AspNetCore.Components;

namespace Charwiki.WebUi.Components.Pages;

/// <summary>
/// Represents the Submit Set page component.
/// </summary>
public partial class SubmitSet
{
    private string ErrorMessage { get; set; } = string.Empty;

    #region Injected Services
    [Inject]
    private ILoomianSetsService LoomianSetsService { get; set; } = default!;

    [Inject]
    private UserTokenService UserTokenService { get; set; } = default!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;
    #endregion

    #region Option storing lists
    private IEnumerable<LoomianResponseDto> Loomians { get; set; } = [];
    private IEnumerable<LoomianAbilityResponseDto> Abilities { get; set; } = [];
    private IEnumerable<LoomianItemResponseDto> Items { get; set; } = [];
    private IEnumerable<LoomianMoveResponseDto> Moves { get; set; } = [];
    private IEnumerable<GameVersionInfoResponseDto> GameVersions { get; set; } = [];
    private IEnumerable<TagResponseDto>? Tags { get; set; } = [];

    private IEnumerable<int> AllowedPersonalityModifiers { get; set; } = new[]
    {
        -20, -10, 10, 20
    };
    #endregion

    [SupplyParameterFromForm]
    private SubmitLoomianSetRequestDto? LoomianSetDto { get; set; }

    private IEnumerable<TagResponseDto> SelectedTags { get; set; } = [];

    #region Methods
    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        Loomians = CachedDataLists.Loomians.OrderBy(l => l.Name);
        Abilities = CachedDataLists.Abilities.OrderBy(a => a.Name);
        Items = CachedDataLists.Items.OrderBy(i => i.Name);
        Moves = CachedDataLists.Moves.OrderBy(m => m.Name);
        GameVersions = CachedDataLists.GameVersionInfos.OrderBy(g => g.VersionCode);
        Tags = CachedDataLists.Tags.OrderBy(t => t.Name);

        SelectedTags = [];

        LoomianSetDto = new()
        {
            TrainingPoints = new(),
            UniquePoints = new()
            {
                Health = 40,
                Energy = 40,
                MeleeAttack = 40,
                MeleeDefense = 40,
                RangedAttack = 40,
                RangedDefense = 40,
                Speed = 40
            },
            PersonalityModifiers = new()
        };
    }

    private async Task SubmitSetAsync()
    {
        if (LoomianSetDto is null)
        {
            return;
        }
        LoomianSetDto.TagsIds = SelectedTags.Select(t => t.Id).ToList();

        string? userToken = await UserTokenService.GetAuthTokenAsync();
        if (userToken is null)
        {
            ErrorMessage = "User token is required to submit a Loomian set.";
            return;
        }

        try
        {
            // Convert the LoomianSetDto to a temporary LoomianSet for pre-validation.
            LoomianSet prevalidations = LoomianSetDto.ToLoomianSet(Guid.Empty);
            OperationResult validationResult = prevalidations.ValidateSet();
            if (validationResult.HasFailed)
            {
                // If validation fails, set the error message and return.
                ErrorMessage = validationResult.UserMessage;
                return;
            }

            // If the Loomian set is valid, proceed to submit it.
            LoomianSet createdSet = await LoomianSetsService.SubmitSetAsync(LoomianSetDto, userToken);
            NavigationManager.NavigateTo($"/loomianset/{createdSet.Id}", forceLoad: true);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"{ex.Message}";
        }
    }

    string newTag = string.Empty;
    private void OnTagSelected()
    {
        Guid tagId = Guid.Parse(newTag);
        if (Tags is null || !Tags.Any())
        {
            return;
        }
        TagResponseDto? tag = Tags.FirstOrDefault(t => t.Id == tagId);
        if (tag == null)
        {
            return;
        }
        // If the tag is already selected, do not add it again.
        if (SelectedTags.Any(t => t.Id == tag.Id))
        {
            return;
        }
        SelectedTags = SelectedTags.Append(tag);
        newTag = string.Empty;
        StateHasChanged();
    }


    private void RemoveTag(TagResponseDto tagToRemove)
    {
        SelectedTags = SelectedTags.Where(t => t.Id != tagToRemove.Id);
        StateHasChanged();
    }
    #endregion
}