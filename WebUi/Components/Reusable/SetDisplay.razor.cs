using Charwiki.ClassLib.Dto.Response;
using Charwiki.ClassLib.Enums;
using Charwiki.ClassLib.Services;
using Charwiki.WebUi.Services;
using Microsoft.AspNetCore.Components;

namespace Charwiki.WebUi.Components.Reusable;

/// <summary>
/// Represents a component for displaying a Loomian set.
/// </summary>
public partial class SetDisplay
{
    /// <summary>
    /// The Loomian set to display.
    /// </summary>
    [Parameter]
    public required LoomianSetResponseDto LoomianSet { get; set; }

    [Inject]
    private UserTokenService UserTokenService { get; set; } = default!;

    [Inject]
    private ILoomianSetsService LoomianSetsService { get; set; } = default!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    private readonly List<LoomianStat> DisplayedStats =
    [
        LoomianStat.Health,
        LoomianStat.Energy,
        LoomianStat.MeleeAttack,
        LoomianStat.RangedAttack,
        LoomianStat.MeleeDefense,
        LoomianStat.RangedDefense,
        LoomianStat.Speed
    ];

    private bool IsAuthenticated = false;

    private int StarRating { get; set; }

    private int PreviousStarRating = -1;

    /// <inheritdoc/>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }
        await UpdatePreviousRating();
        StateHasChanged();
    }

    private async Task UpdatePreviousRating(bool forceWithNoRatings = false)
    {
        IsAuthenticated = await UserTokenService.IsAuthenticatedAsync();

        // If the user is authenticated, check if they have rated the Loomian set
        if (IsAuthenticated && LoomianSet.RatingsCount != 0 || forceWithNoRatings)
        {
            string? token = await UserTokenService.GetAuthTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                StarRatingResponseDto? myRating = await LoomianSetsService.GetMyRatingAsync(LoomianSet.Id, token);
                if (myRating != null)
                {
                    PreviousStarRating = myRating.Stars;
                    Console.WriteLine($"Previous rating for set {LoomianSet.Id}: {PreviousStarRating}");
                }
            }
        }
    }

    private void SetSelectedStarRating(int rating)
    {
        StarRating = rating;
    }

    private async Task RateSetAsync()
    {
        string? token = await UserTokenService.GetAuthTokenAsync();
        if (string.IsNullOrEmpty(token))
        {
            // Rating buttons are not enalbled if the user is not authenticated
            return;
        }
        await LoomianSetsService.RateLoomianSetAsync(LoomianSet.Id, StarRating, token);

        await UpdatePreviousRating(true);
        StateHasChanged();
    }
}