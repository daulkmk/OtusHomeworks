using System;
using Lessons.MetaGame.Inventory;
using UnityEngine;

namespace Crafting
{
    [Serializable]
    public struct Ingredient
    {
        [field: SerializeField]
        public InventoryItemConfig InventoryItemConfig { get; private set; }

        [field: SerializeField]
        public int Count { get; private set; }

        public Ingredient(InventoryItemConfig inventoryItemConfig, int count)
        {
            InventoryItemConfig = inventoryItemConfig;
            Count = count;
        }
    }
}