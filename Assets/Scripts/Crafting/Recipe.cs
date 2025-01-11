using System.Collections;
using System.Collections.Generic;
using Lessons.MetaGame.Inventory;
using UnityEngine;

namespace Crafting
{
    [CreateAssetMenu(fileName = "CraftingRecipe", menuName = "Lessons/New CraftingRecipe")]
    public class Recipe : ScriptableObject
    {
        [field: SerializeField]
        public List<Ingredient> Ingredients { get; set; } = new();

        [field: SerializeField]
        public InventoryItemConfig ResultItem { get;  set; }
    }
}