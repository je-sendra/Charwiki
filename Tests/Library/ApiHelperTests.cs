using KellermanSoftware.CompareNetObjects;
using Microsoft.AspNetCore.JsonPatch;
using NUnit.Framework;
using VewTech.Charwiki.Library.Models;

namespace VewTech.Charwiki.Tests.Library;

public class ApiHelperTests
{
    [Test]
    public async Task HeldItemTests()
    {
        // GetAll test
        await HeldItem.ApiHelper.GetAll();

        // Post test
        var heldItem = new HeldItem()
        {
            Id = Guid.NewGuid(),
            Name = "TEST"
        };
        await HeldItem.ApiHelper.Post(heldItem);

        // GetById test
        var heldItem2 = await HeldItem.ApiHelper.GetById(heldItem.Id);
        var compareLogic = new CompareLogic();
        var comparisonResult = compareLogic.Compare(heldItem, heldItem2);
        Assert.That(comparisonResult.AreEqual);

        // Patch test
        var heldItemPatch = new JsonPatchDocument<HeldItem>();
        heldItemPatch.Replace(e => e.Name, "TEST2");
        await HeldItem.ApiHelper.Patch(heldItem2.Id, heldItemPatch);
        heldItem2 = await HeldItem.ApiHelper.GetById(heldItem2.Id);
        Assert.That(heldItem2.Name, Is.EqualTo("TEST2"));

        // Delete test
        await HeldItem.ApiHelper.Delete(heldItem2.Id);
    }


}