using Game.GameEngine.Mechanics;
using Lessons.MetaGame.Inventory;
using System.Collections.Generic;
using System;
using System.Linq;

public class Equipment : IEquipment
{
    private readonly Dictionary<EquipSlot, InventoryItem> _equpedItems = new();

    public event Action<EquipSlot, InventoryItem> OnItemEquipped;
    public event Action<EquipSlot, InventoryItem> OnItemUnequipped;

    public IEnumerable<KeyValuePair<EquipSlot, InventoryItem>> EquippedItems
    {
        get => _equpedItems.Where(x => x.Value != null);
    }

    public Equipment()
    {
        foreach (var equipType in (EquipSlot[])Enum.GetValues(typeof(EquipSlot)))
        {
            _equpedItems.Add(equipType, null);
        }
    }

    public bool CanEquip(InventoryItem inventoryItem)
    {
        return inventoryItem.Flags.HasFlag(InventoryItemFlags.EQUPPABLE);
    }

    /// <summary>
    /// Equip item. Replace if no slots available.
    /// </summary>
    /// <param name="inventoryItem">Item to equip</param>
    public void Equip(InventoryItem inventoryItem, out InventoryItem unequippedItem)
    {
        unequippedItem = default;

        if (CanEquip(inventoryItem))
        {
            var equipmentType = GetItemEquipType(inventoryItem);
            if (!TryGetFreeSlotForType(equipmentType, out var slot))
            {
                slot = GetSlotsForEquipType(equipmentType).First();
                unequippedItem = ClearSlot(slot);
            }

            SetItemInSlot(slot, inventoryItem);
        }
        else
        {
            throw new Exception("Cannot Equip " + inventoryItem.Name);
        }
    }

    public bool Unequip(InventoryItem inventoryItem)
    {
        var equipmentType = GetItemEquipType(inventoryItem);

        foreach (var slot in GetSlotsForEquipType(equipmentType))
        {
            if (_equpedItems[slot] == inventoryItem)
            {
                ClearSlot(slot);
                return true;
            }
        }
        return false;
    }

    private void SetItemInSlot(EquipSlot slot, InventoryItem inventoryItem)
    {
        _equpedItems[slot] = inventoryItem;
        OnItemEquipped?.Invoke(slot, inventoryItem);
    }

    private InventoryItem ClearSlot(EquipSlot slot)
    {
        var unequippedItem = _equpedItems[slot];
        _equpedItems[slot] = null;

        if (unequippedItem != null)
        {
            OnItemUnequipped?.Invoke(slot, unequippedItem);
        }

        return unequippedItem;
    }

    private bool TryGetFreeSlotForType(EquipType equipType, out EquipSlot freeSlot)
    {
        foreach (var slot in GetSlotsForEquipType(equipType))
        {
            if (_equpedItems[slot] == null)
            {
                freeSlot = slot;
                return true;
            }
        }

        freeSlot = default;
        return false;
    }

    private static EquipType GetItemEquipType(InventoryItem inventoryItem)
    {
        return inventoryItem.GetComponent<Component_EquipType>().Type;
    }

    /// <summary></summary>
    /// <param name="equipType">Equipment type of the inventory item.</param>
    /// <returns>Types of slots that can accept this type of equipment.</returns>
    /// <exception cref="NotImplementedException"></exception>
    private static IEnumerable<EquipSlot> GetSlotsForEquipType(EquipType equipType)
    {
        switch (equipType)
        {
            case EquipType.BODY:
                yield return EquipSlot.BODY;
                break;
            case EquipType.HEAD:
                yield return EquipSlot.HEAD;
                break;
            case EquipType.HANDS:
                yield return EquipSlot.HAND_LEFT;
                yield return EquipSlot.HAND_RIGHT;
                break;
            case EquipType.LEGS:
                yield return EquipSlot.LEGS;
                break;
            default:
                throw new NotImplementedException("Slot is not implemented for equipment type: " + equipType);
        }
    }
}
