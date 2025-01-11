using System;
using System.Linq;
using Crafting;
using Lessons.MetaGame.Inventory;
using NUnit.Framework;

[TestFixture]
public abstract partial class CraftingTests<T> where T : ICraftSystem
{
    protected IInventory _inventory;
    protected ICraftSystem _craftSystem;

    [SetUp]
    public void SetUp()
    {
        _inventory = SetUpNewInventory();
        _craftSystem = SetUpNewCraftSystem();
    }

    protected abstract IInventory SetUpNewInventory();
    protected abstract ICraftSystem SetUpNewCraftSystem();

    #region CanCraft
    [Test]
    [Category(nameof(ICraftSystem.CanCraft))]
    public void WhenCanCraft_AndHasAllItemsInInventory_ThenReturnsTrue()
    {
        //Arrange
        var swordRecipe = Helper.CreateSwordRecipe();
        Helper.AddIngredientsToInventory(_inventory, swordRecipe.Ingredients);

        //Act
        bool canCraft = _craftSystem.CanCraft(swordRecipe);

        //Assert
        Assert.IsTrue(canCraft);
    }

    [Test]
    [Category(nameof(ICraftSystem.CanCraft))]
    public void WhenCanCraft_AndHasNoItemsInInventory_ThenReturnsFalse()
    {
        //Arrange
        var swordRecipe = Helper.CreateSwordRecipe();

        //Act
        bool canCraft = _craftSystem.CanCraft(swordRecipe);

        //Assert
        Assert.IsFalse(canCraft);
    }

    [Test]
    [Category(nameof(ICraftSystem.CanCraft))]
    public void WhenCanCraft_AndHasSomeItemsInInventory_ThenReturnsFalse()
    {
        //Arrange
        var swordRecipe = Helper.CreateSwordRecipe();
        var ingredient = swordRecipe.Ingredients.First();
        Helper.AddIngredientToInventory(_inventory, ingredient);

        //Act
        bool canCraft = _craftSystem.CanCraft(swordRecipe);

        //Assert
        Assert.IsFalse(canCraft);
    }

    [Test]
    [Category(nameof(ICraftSystem.CanCraft))]
    public void WhenCanCraft_AndRecipeIsNull_ThenThrowArgumentNullException()
    {
        //Arrange
        Recipe recipe = null;

        //Act
        bool exceptionThrown = false;
        try
        {
            _craftSystem.CanCraft(recipe);
        }
        catch (ArgumentNullException)
        {
            exceptionThrown = true;
        }

        //Assert
        Assert.IsTrue(exceptionThrown);
    }

    #endregion

    #region Craft

    [Test]
    [Category(nameof(ICraftSystem.Craft))]
    public void WhenCraft_AndHasAllItemsInInventory_ThenCreatesNewItemAndRemovesIngredients()
    {
        //Arrange
        var swordRecipe = Helper.CreateSwordRecipe();
        Helper.AddIngredientsToInventory(_inventory, swordRecipe.Ingredients);

        //Act
        _craftSystem.Craft(swordRecipe);

        int resultItemCount = _inventory.GetCount(swordRecipe.ResultItem.item.Name);
        bool allIngredientsCountIsZero = swordRecipe.Ingredients.TrueForAll(
            x => _inventory.GetCount(x.InventoryItemConfig.item.Name) == 0
        );

        //Assert
        Assert.AreEqual(1, resultItemCount);
        Assert.IsTrue(allIngredientsCountIsZero);
    }

    [Test]
    [Category(nameof(ICraftSystem.Craft))]
    public void WhenCraft_AndHasMoreItemsInInventory_ThenCreatesNewItem_AndRemovesIngredients_AndSomeIngredientsLeft()
    {
        //Arrange
        var swordRecipe = Helper.CreateSwordRecipe();
        Helper.AddIngredientsToInventory(_inventory, swordRecipe.Ingredients);
        Helper.AddIngredientsToInventory(_inventory, swordRecipe.Ingredients); //Add ingredients twice

        //Act
        _craftSystem.Craft(swordRecipe);

        int resultItemCount = _inventory.GetCount(swordRecipe.ResultItem.item.Name);
        bool allIngredientsLeft = swordRecipe.Ingredients.TrueForAll(
            x => _inventory.GetCount(x.InventoryItemConfig.item.Name) == x.Count
        );

        //Assert
        Assert.AreEqual(1, resultItemCount);
        Assert.IsTrue(allIngredientsLeft);
    }

    [Test]
    [Category(nameof(ICraftSystem.Craft))]
    public void WhenCraft_AndHasNotItemsInInventory_ThenThrowsCannotCraftException()
    {
        //Arrange
        var swordRecipe = Helper.CreateSwordRecipe();

        //Act
        bool exceptionThrown = false;
        try
        {
            _craftSystem.Craft(swordRecipe);
        }
        catch (CannotCraftException)
        {
            exceptionThrown = true;
        }

        //Assert
        Assert.IsTrue(exceptionThrown);
    }

    [Test]
    [Category(nameof(ICraftSystem.Craft))]
    public void WhenCraft_AndHasSomeItemsInInventory_ThenThrowsCannotCraftException()
    {
        //Arrange
        var swordRecipe = Helper.CreateSwordRecipe();
        var ingredient = swordRecipe.Ingredients.First();
        Helper.AddIngredientToInventory(_inventory, ingredient);

        //Act
        bool exceptionThrown = false;
        try
        {
            _craftSystem.Craft(swordRecipe);
        }
        catch (CannotCraftException)
        {
            exceptionThrown = true;
        }

        //Assert
        Assert.IsTrue(exceptionThrown);
    }

    [Test]
    [Category(nameof(ICraftSystem.Craft))]
    public void WhenCraft_AndHasOtherItemsInInventory_ThenThrowsCannotCraftException_AndOtherItemsLeft()
    {
        //Arrange
        var swordRecipe = Helper.CreateSwordRecipe();
        var otherItems = new (string itemName, int count)[]
        {
            (Helper.NAME_ARMOR, 1),
            (Helper.NAME_MEAT, 2),
            (Helper.NAME_HEALING_POTION, 3),
        };

        Helper.AddItemsToInventory(_inventory, otherItems);

        //Act
        bool exceptionThrown = false;
        try
        {
            _craftSystem.Craft(swordRecipe);
        }
        catch (CannotCraftException)
        {
            exceptionThrown = true;
        }

        bool allOtherItemsLeft = Array.TrueForAll(otherItems,
            x => _inventory.GetCount(x.itemName) == x.count
        );

        //Assert
        Assert.IsTrue(exceptionThrown);
        Assert.IsTrue(allOtherItemsLeft);
    }

    [Test]
    [Category(nameof(ICraftSystem.Craft))]
    public void WhenCraft_AndRecipeIsNull_ThenThrowArgumentNullException()
    {
        //Arrange
        Recipe recipe = null;

        //Act
        bool exceptionThrown = false;
        try
        {
            _craftSystem.Craft(recipe);
        }
        catch (ArgumentNullException)
        {
            exceptionThrown = true;
        }

        //Assert
        Assert.IsTrue(exceptionThrown);
    }
    #endregion
}
