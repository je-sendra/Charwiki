using Charwiki.ClassLib.Dto.Request;
using Charwiki.ClassLib.Enums;
using Charwiki.ClassLib.Extensions;
using Charwiki.ClassLib.Models;
using Charwiki.ClassLib.Models.OperationResult;
using Charwiki.ClassLib.Services;
using Charwiki.WebUi.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

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

    private IEnumerable<int> AllowedPersonalityModifiers { get; set; } = new[]
    {
        -20, -10, 10, 20
    };
    #endregion

    [SupplyParameterFromForm]
    private SubmitLoomianSetRequestDto? LoomianSetDto { get; set; }

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
    #endregion
}