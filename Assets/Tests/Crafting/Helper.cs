using System.Collections.Generic;
using Crafting;
using Lessons.MetaGame.Inventory;
using UnityEngine;

public partial class CraftingTests<T>
{
    private static class Helper
    {
        public const string NAME_SWORD = "sword";
        public const string NAME_IRON = "iron";
        public const string NAME_STICK = "stick";

        public const string NAME_ARMOR = "armor";
        public const string NAME_MEAT = "meat";
        public const string NAME_HEALING_POTION = "healing_potion";

        public static Recipe CreateSwordRecipe()
        {
            var sword = CreateItemConfig(NAME_SWORD);

            var iron = CreateItemConfig(NAME_IRON);
            var stick = CreateItemConfig(NAME_STICK);

            var recipe = ScriptableObject.CreateInstance<Recipe>();

            recipe.ResultItem = sword;

            recipe.Ingredients.Add(new Ingredient(iron, 2));
            recipe.Ingredients.Add(new Ingredient(stick, 1));

            return recipe;
        }

        public static void AddIngredientsToInventory(IInventory inventory, IEnumerable<Ingredient> ingredients)
        {
            foreach (var ingredient in ingredients)
            {
                for (int i = 0; i < ingredient.Count; i++)
                {
                    AddIngredientToInventory(inventory, ingredient);
                }
            }
        }

        public static void AddIngredientToInventory(IInventory inventory, Ingredient ingredient)
        {
            var item = ingredient.InventoryItemConfig.item.Clone();
            inventory.AddItem(item);
        }

        public static void AddItemsToInventory(IInventory inventory, params (string itemName, int count)[] items)
        {
            foreach (var (itemName, count) in items)
            {
                AddItemToInventory(inventory, itemName, count);
            }
        }

        public static void AddItemToInventory(IInventory inventory, string itemName, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var item = CreateItem(itemName);
                inventory.AddItem(item);
            }
        }

        public static InventoryItem CreateItem(string itemName)
        {
            return new InventoryItem(name: itemName,
                flags: InventoryItemFlags.NONE,
                metadata: new InventoryItemMetadata(),
                components: new object[0]
            );
        }

        public static InventoryItemConfig CreateItemConfig(string itemName)
        {
            var config = ScriptableObject.CreateInstance<InventoryItemConfig>();
            config.item = CreateItem(itemName);

            return config;
        }
    }
}
