using Charwiki.ClassLib.Enums;
using Charwiki.ClassLib.Models;
using Charwiki.ClassLib.Services;
using Microsoft.AspNetCore.Components;

namespace Charwiki.WebUi.Components.Reusable;

public partial class SetDisplay
{
    [Parameter]
    public required LoomianSet LoomianSet { get; set; }

    [Inject]
    private ILoomiansService LoomiansService { get; set; } = default!;
    [Inject]
    private ILoomianAbilitiesService LoomianAbilitiesService { get; set; } = default!;
    [Inject]
    private ILoomianItemsService LoomianItemsService { get; set; } = default!;
    [Inject]
    private ILoomianMovesService LoomianMovesService { get; set; } = default!;
    [Inject]
    private IUserService UsersService { get; set; } = default!;

    private readonly List<LoomianStat> DisplayedStats =
    [
        LoomianStat.Health,
        LoomianStat.MeeleeAttack,
        LoomianStat.RangedAttack,
        LoomianStat.MeeleeDefense,
        LoomianStat.RangedDefense,
        LoomianStat.Speed
    ];


    /// <inheritdoc/>
    protected override async Task OnParametersSetAsync()
    {
        // If the LoomianSet Loomian is null, fetch it from the service
        if (LoomianSet.Loomian == null)
        {
            LoomianSet.Loomian = await LoomiansService.GetByIdAsync(LoomianSet.LoomianId);
        }

        // If the LoomianSet Item is null and the Loomian is holding an item, fetch it from the service
        if (LoomianSet.Item == null && LoomianSet.ItemId != null)
        {
            LoomianSet.Item = await LoomianItemsService.GetByIdAsync((Guid)LoomianSet.ItemId);
        }

        // If the LoomianSet Ability is null, fetch it from the service
        if (LoomianSet.Ability == null)
        {
            LoomianSet.Ability = await LoomianAbilitiesService.GetByIdAsync(LoomianSet.LoomianAbilityId);
        }

        // If the LoomianSet Move1 is null and the Loomian has a Move1, fetch it from the service
        if (LoomianSet.Move1 == null && LoomianSet.Move1Id != null)
        {
            LoomianSet.Move1 = await LoomianMovesService.GetByIdAsync((Guid)LoomianSet.Move1Id);
        }

        // If the LoomianSet Move2 is null and the Loomian has a Move2, fetch it from the service
        if (LoomianSet.Move2 == null && LoomianSet.Move2Id != null)
        {
            LoomianSet.Move2 = await LoomianMovesService.GetByIdAsync((Guid)LoomianSet.Move2Id);
        }

        // If the LoomianSet Move3 is null and the Loomian has a Move3, fetch it from the service
        if (LoomianSet.Move3 == null && LoomianSet.Move3Id != null)
        {
            LoomianSet.Move3 = await LoomianMovesService.GetByIdAsync((Guid)LoomianSet.Move3Id);
        }

        // If the LoomianSet Move4 is null and the Loomian has a Move4, fetch it from the service
        if (LoomianSet.Move4 == null && LoomianSet.Move4Id != null)
        {
            LoomianSet.Move4 = await LoomianMovesService.GetByIdAsync((Guid)LoomianSet.Move4Id);
        }

        // If the LoomianSet Creator is null, fetch it from the service
        if (LoomianSet.Creator == null)
        {
            LoomianSet.Creator = await UsersService.GetByIdAsync(LoomianSet.CreatorId);
        }

        // If the LoomianSet Approver is null and the LoomianSet is approved, fetch it from the service
        if (LoomianSet.Approver == null && LoomianSet.Approved && LoomianSet.ApproverId != null)
        {
            LoomianSet.Approver = await UsersService.GetByIdAsync(LoomianSet.ApproverId.Value);
        }
    }
}