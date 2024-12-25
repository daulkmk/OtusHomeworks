using Game.GameEngine.Mechanics;
using Lessons.MetaGame.Inventory;
using UnityEngine;
using Sirenix.OdinInspector;

public class Equipment
{
    [ShowInInspector, ReadOnly]
    private ListInventory _listInventory = new();

    public bool CanEquip(InventoryItem inventoryItem)
    {
        return inventoryItem.Flags.HasFlag(InventoryItemFlags.EQUPPABLE);
    }

    public bool IsSlotAvailable(InventoryItem inventoryItem)
    {
        var equipType = inventoryItem.GetComponent<Component_EquipType>().Type;
        return IsSlotAvailable(equipType);
    }

    public bool IsSlotAvailable(EquipType equipType)
    {
        return _listInventory
            .GetItems()
            .FindIndex(x => x.GetComponent<Component_EquipType>().Type == equipType) == -1;
    }

    public bool TryGetOtherItemInSlot(InventoryItem inventoryItem, out InventoryItem otherItem)
    {
        var equipType = inventoryItem.GetComponent<Component_EquipType>().Type;
        otherItem = GetItemInSlot(equipType);
        return otherItem != null;
    }

    public InventoryItem GetItemInSlot(EquipType equipType)
    {
        return _listInventory
            .GetItems()
            .Find(x => x.GetComponent<Component_EquipType>().Type == equipType);
    }

    public void Equip(InventoryItem inventoryItem)
    {
        if (CanEquip(inventoryItem))
        {
            _listInventory.AddItem(inventoryItem);
        }
        else
        {
            Debug.Log("Cannot Equip " + inventoryItem.Name);
        }
    }

    public void Unequip(InventoryItem inventoryItem)
    {
        _listInventory.RemoveItem(inventoryItem);
    }

    public void AddObserver(IInventoryObserver observer)
    {
        _listInventory.AddObserver(observer);
    }

    public void RemoveObserver(IInventoryObserver observer)
    {
        _listInventory.RemoveObserver(observer);
    }
}
