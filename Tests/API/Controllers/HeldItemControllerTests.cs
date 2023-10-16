using VewTech.Charwiki.API.Controllers;

namespace VewTech.Charwiki.Tests.API.Controllers;

/// <summary>
/// The tests for the HeldItemController.
/// </summary>
[TestClass]
public class HeldItemControllerTests
{
    /// <summary>
    /// The HeldItemController to use.
    /// </summary>
    private readonly HeldItemController _heldItemController = new();

    /// <summary>
    /// A test for the HeldItemController.Test() method.
    /// </summary>
    [TestMethod]
    public void Get()
    {
        _heldItemController.Get();
    }
}