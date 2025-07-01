using Charwiki.ClassLib.Dto.Request;
using Charwiki.ClassLib.Enums;
using Charwiki.ClassLib.Extensions;
using Charwiki.ClassLib.Models;
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
    private ILoomiansService LoomianService { get; set; } = default!;

    [Inject]
    private ILoomianAbilitiesService LoomianAbilityService { get; set; } = default!;

    [Inject]
    private ILoomianItemsService LoomianItemService { get; set; } = default!;

    [Inject]
    private ILoomianMovesService LoomianMoveService { get; set; } = default!;

    [Inject]
    private IGameVersionInfosService GameVersionInfosService { get; set; } = default!;

    [Inject]
    private ILoomianSetsService LoomianSetsService { get; set; } = default!;

    [Inject]
    private UserTokenService UserTokenService { get; set; } = default!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;
    #endregion

    #region Option storing lists
    private IEnumerable<Loomian> Loomians { get; set; } = [];
    private IEnumerable<LoomianAbility> Abilities { get; set; } = [];
    private IEnumerable<LoomianItem> Items { get; set; } = [];
    private IEnumerable<LoomianMove> Moves { get; set; } = [];
    private IEnumerable<GameVersionInfo> GameVersions { get; set; } = [];
    #endregion

    #region Selected values
    private Guid? SelectedLoomianId { get; set; } = null;
    private Guid? SelectedAbilityId { get; set; } = null;
    private Guid? SelectedItemId { get; set; } = null;
    private Guid? SelectedGameVersionId { get; set; } = null;

    private Guid? SelectedMove1Id { get; set; } = null;
    private Guid? SelectedMove2Id { get; set; } = null;
    private Guid? SelectedMove3Id { get; set; } = null;
    private Guid? SelectedMove4Id { get; set; } = null;

    private string Title { get; set; } = string.Empty;
    private string Explanation { get; set; } = string.Empty;
    private string Strategy { get; set; } = string.Empty;
    private List<string> Strengths { get; set; } = [];
    private List<string> Weaknesses { get; set; } = [];
    private string OtherOptions { get; set; } = string.Empty;

    private List<ValueToStatAssignment> PersonalityModifiers { get; set; } = [
        new ValueToStatAssignment { Stat = LoomianStat.Health, Value = 0 },
        new ValueToStatAssignment { Stat = LoomianStat.MeleeAttack, Value = 0 },
        new ValueToStatAssignment { Stat = LoomianStat.RangedAttack, Value = 0 },
        new ValueToStatAssignment { Stat = LoomianStat.MeleeDefense, Value = 0 },
        new ValueToStatAssignment { Stat = LoomianStat.RangedDefense, Value = 0 },
        new ValueToStatAssignment { Stat = LoomianStat.Energy, Value = 0 },
        new ValueToStatAssignment { Stat = LoomianStat.Speed, Value = 0 }
    ];
    private List<ValueToStatAssignment> TrainingPoints { get; set; } = [
        new ValueToStatAssignment { Stat = LoomianStat.Health, Value = 0 },
        new ValueToStatAssignment { Stat = LoomianStat.MeleeAttack, Value = 0 },
        new ValueToStatAssignment { Stat = LoomianStat.RangedAttack, Value = 0 },
        new ValueToStatAssignment { Stat = LoomianStat.MeleeDefense, Value = 0 },
        new ValueToStatAssignment { Stat = LoomianStat.RangedDefense, Value = 0 },
        new ValueToStatAssignment { Stat = LoomianStat.Energy, Value = 0 },
        new ValueToStatAssignment { Stat = LoomianStat.Speed, Value = 0 }
    ];
    private List<ValueToStatAssignment> UniquePoints { get; set; } = [
        new ValueToStatAssignment { Stat = LoomianStat.Health, Value = 0 },
        new ValueToStatAssignment { Stat = LoomianStat.MeleeAttack, Value = 0 },
        new ValueToStatAssignment { Stat = LoomianStat.RangedAttack, Value = 0 },
        new ValueToStatAssignment { Stat = LoomianStat.MeleeDefense, Value = 0 },
        new ValueToStatAssignment { Stat = LoomianStat.RangedDefense, Value = 0 },
        new ValueToStatAssignment { Stat = LoomianStat.Energy, Value = 0 },
        new ValueToStatAssignment { Stat = LoomianStat.Speed, Value = 0 }
    ];
    #endregion

    #region Methods
    /// <inheritdoc/>
    protected async override Task OnInitializedAsync()
    {
        Loomians = await LoomianService.GetAllAsync();
        Loomians = Loomians.OrderBy(l => l.Name);
        Abilities = await LoomianAbilityService.GetAllAsync();
        Abilities = Abilities.OrderBy(a => a.Name);
        Items = await LoomianItemService.GetAllAsync();
        Items = Items.OrderBy(i => i.Name);
        Moves = await LoomianMoveService.GetAllAsync();
        Moves = Moves.OrderBy(m => m.Name);
        GameVersions = await GameVersionInfosService.GetAllAsync();
    }

    private async Task SubmitSetAsync()
    {
        SubmitLoomianSetRequestDto? set = GenerateLoomianSetDto();
        if (set is null)
        {
            return;
        }

        string? userToken = await UserTokenService.GetAuthTokenAsync();
        if (userToken is null)
        {
            ErrorMessage = "User token is required to submit a Loomian set.";
            return;
        }

        try
        {
            // Convert the LoomianSetDto to a temporary LoomianSet for pre-validation.
            LoomianSet prevalidations = set.ToLoomianSet(Guid.Empty);
            prevalidations.EnsureSetIsValid();

            // If the Loomian set is valid, proceed to submit it.
            LoomianSet createdSet = await LoomianSetsService.SubmitSetAsync(set!, userToken);
            NavigationManager.NavigateTo($"/loomianset/{createdSet.Id}", forceLoad: true);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"{ex.Message}";
            return;
        }
    }

    private SubmitLoomianSetRequestDto? GenerateLoomianSetDto()
    {
        if (SelectedLoomianId is null)
        {
            ErrorMessage = "Please select a Loomian.";
            return null;
        }

        if (SelectedAbilityId is null)
        {
            ErrorMessage = "Please select an ability.";
            return null;
        }

        if (SelectedGameVersionId is null)
        {
            ErrorMessage = "Please select a game version.";
            return null;
        }

        if (string.IsNullOrEmpty(Title))
        {
            ErrorMessage = "Please provide a title for the Loomian set.";
            return null;
        }

        return new SubmitLoomianSetRequestDto()
        {
            LoomianId = SelectedLoomianId.Value,
            PersonalityModifiers = PersonalityModifiers,
            AbilityId = SelectedAbilityId.Value,
            ItemId = SelectedItemId,
            TrainingPoints = TrainingPoints,
            UniquePoints = UniquePoints,
            Move1Id = SelectedMove1Id,
            Move2Id = SelectedMove2Id,
            Move3Id = SelectedMove3Id,
            Move4Id = SelectedMove4Id,
            Title = Title,
            Explanation = Explanation,
            Strategy = Strategy,
            Strengths = Strengths,
            Weaknesses = Weaknesses,
            OtherOptions = OtherOptions,
            GameVersionInfoId = SelectedGameVersionId.Value
        };
    }
    #endregion
}