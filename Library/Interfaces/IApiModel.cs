using System.ComponentModel.DataAnnotations;

namespace VewTech.Charwiki.Library.Interfaces;

public interface IApiModel
{
    /// <summary>
    /// The unique identifier for the resource.
    /// </summary>
    [Required]
    public Guid Id { get; set; }
}