
using Charwiki.ClassLib.Dto.Response;
using Microsoft.AspNetCore.Components;

namespace Charwiki.WebUi.Components.Reusable;

/// <summary>
/// Represents a card component that displays a Loomian set.
/// This component is used to showcase a Loomian set with its title and a brief explanation.
/// </summary>
public partial class SetCard
{
    /// <summary>
    /// The Loomian set to display.
    /// This parameter is required and must be provided when using the component.
    /// </summary>
    [Parameter]
    public required LoomianSetResponseDto Set { get; set; }

    private string DisplayTitle = string.Empty;
    private string DisplayExplanation = string.Empty;

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        if (Set.Loomian == null)
        {
            DisplayTitle = $"{Set.Title}";
        }
        else
        {
            DisplayTitle = $"{Set.Title} {Set.Loomian.Name}";
        }

        // Truncate the explanation to X characters
        if (string.IsNullOrEmpty(Set.ShortDescription))
        {
            DisplayExplanation = "No description available.";
        }
        else
        {
            var allowedLength = 70;

            // If the explanation is longer than the allowed length, truncate it
            if (Set.ShortDescription.Length > allowedLength)
            {
                DisplayExplanation = Set.ShortDescription.Substring(0, allowedLength) + "...";
            }
            // Else, just display the explanation
            else
            {
                DisplayExplanation = Set.ShortDescription;
            }
        }
    }
}