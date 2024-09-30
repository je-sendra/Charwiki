using Charwiki.ClassLib.Enums;

namespace Charwiki.ClassLib.Models;

/// <summary>
/// Represents a value-to-stat assignment for a Loomian. This is used for TPs, UPs and personalities, among other things.
/// </summary>
public class ValueToStatAssignment
{
    /// <summary>
    /// The value of the assignment.
    /// </summary>
    public int Value { get; set; }

    /// <summary>
    /// The stat that the value is assigned to.
    /// </summary>
    public LoomianStat Stat { get; set; }
}