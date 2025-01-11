using System;
using Crafting;
using Lessons.MetaGame.Inventory;
using NUnit.Framework;

public class CraftSystemWithListInventoryTests : CraftingTests<CraftSystem>
{
    protected override IInventory SetUpNewInventory()
    {
        return new ListInventory();
    }
    
    protected override ICraftSystem SetUpNewCraftSystem()
    {
        return new CraftSystem(_inventory);
    }

    [Test]
    public void WhenCreateCraftSystemInstance_AndInventoryIsNull_ThenThrowArgumentNullException()
    {
        //Arrange
        IInventory inventory = null;

        //Act
        bool exceptionThrown = false;
        try
        {
            new CraftSystem(inventory);
        }
        catch (ArgumentNullException)
        {
            exceptionThrown = true;
        }

        //Assert
        Assert.IsTrue(exceptionThrown);
    }
}
