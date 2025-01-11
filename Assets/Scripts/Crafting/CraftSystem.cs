using System;
using Lessons.MetaGame.Inventory;

namespace Crafting
{
    public class CraftSystem : ICraftSystem
    {
        private readonly IInventory _inventory;

        public CraftSystem(IInventory inventory)
        {
            _inventory = inventory ?? throw new ArgumentNullException($"{nameof(inventory)}");
        }

        public bool CanCraft(Recipe recipe)
        {
            ValidateRecipe(recipe);

            return recipe.Ingredients.TrueForAll(
                x => _inventory.GetCount(x.InventoryItemConfig.item.Name) >= x.Count
            );
        }

        public void Craft(Recipe recipe)
        {
            if (!CanCraft(recipe))
            {
                throw new CannotCraftException();
            }

            foreach (var ingredient in recipe.Ingredients)
            {
                _inventory.RemoveItems(ingredient.InventoryItemConfig.item.Name, ingredient.Count);
            }

            _inventory.AddItem(recipe.ResultItem.item.Clone());
        }

        private void ValidateRecipe(Recipe recipe)
        {
            if (recipe == null)
            {
                throw new ArgumentNullException($"{nameof(recipe)}");
            }
        }
    }
}