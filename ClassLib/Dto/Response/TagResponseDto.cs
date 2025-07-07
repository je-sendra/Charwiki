using Charwiki.ClassLib.Models;

namespace Charwiki.ClassLib.Dto.Response
{
    /// <summary>
    /// Represents a tag associated with Loomian sets.
    /// Tags can be used to categorize or label Loomian sets for easier identification and organization.
    /// </summary>
    public class TagResponseDto
    {
        /// <summary>
        /// The unique identifier of the tag.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the tag.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The description of the tag.
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}