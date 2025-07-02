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
        public string Name { get; set; } = null!;

        /// <summary>
        /// The description of the tag.
        /// </summary>
        public string Description { get; set; } = null!;

        /// <summary>
        /// Default constructor for serialization purposes.
        /// </summary>
        public TagResponseDto() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TagResponseDto"/> class from a <see cref="Tag"/> model.
        /// </summary>
        /// <param name="tag"></param>
        public TagResponseDto(Tag tag)
        {
            Id = tag.Id;
            Name = tag.Name;
            Description = tag.Description;
        }
    }
}