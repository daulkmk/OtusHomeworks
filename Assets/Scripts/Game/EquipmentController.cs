using System;
using Lessons.MetaGame.Inventory;
using VContainer.Unity;

/// <summary>
/// Controls equipment-inventory interactions.
/// </summary>
public class EquipmentController : IInitializable, IDisposable
{
    private readonly IEquipment _equipment;
    private readonly EquipmentPresenter _equipmentPresenter;

    private readonly IInventory _inventory;
    private readonly ListInventoryPresenter _inventoryPresenter;

    public EquipmentController(IEquipment equipment, EquipmentPresenter equipmentPresenter,
        IInventory inventory, ListInventoryPresenter inventoryPresenter)
    {
        _equipment = equipment;
        _equipmentPresenter = equipmentPresenter;

        _inventory = inventory;
        _inventoryPresenter = inventoryPresenter;
    }

    void IInitializable.Initialize()
    {
        _inventoryPresenter.OnItemClicked += OnInventoryItemClicked;
        _equipmentPresenter.OnItemClicked += OnEquippedItemClicked;
    }

    private void OnInventoryItemClicked(InventoryItem inventoryItem)
    {
        if (!_equipment.CanEquip(inventoryItem))
            return;

        _equipment.Equip(inventoryItem, out var unequippedItem);
        _inventory.RemoveItem(inventoryItem);

        if (unequippedItem != null)
        {
            _inventory.AddItem(unequippedItem);
        }
    }

    private void OnEquippedItemClicked(InventoryItem inventoryItem)
    {
        if (_equipment.Unequip(inventoryItem))
        {
            _inventory.AddItem(inventoryItem);
        }
    }

    void IDisposable.Dispose()
    {
        if (_equipmentPresenter != null)
        {
            _equipmentPresenter.OnItemClicked -= OnEquippedItemClicked;
        }
        if (_inventoryPresenter != null)
        {
            _inventoryPresenter.OnItemClicked -= OnInventoryItemClicked;
        }
    }
}