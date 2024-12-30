using Lessons.MetaGame.Inventory;
using System.Collections.Generic;
using System;

public interface IEquipment
{
    event Action<EquipSlot, InventoryItem> OnItemEquipped;
    event Action<EquipSlot, InventoryItem> OnItemUnequipped;

    IEnumerable<KeyValuePair<EquipSlot, InventoryItem>> EquippedItems { get; }

    bool CanEquip(InventoryItem inventoryItem);
    void Equip(InventoryItem inventoryItem, out InventoryItem unequippedItem);
    bool Unequip(InventoryItem inventoryItem);
}
