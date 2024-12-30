using Lessons.MetaGame.Inventory;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

public class EquipmentDebug : MonoBehaviour
{
    [Inject] private IInventory _inventory;

    [Button]
    private void AddItem(InventoryItemConfig config)
    {
        var prefab = config.item;
        var inventoryItem = prefab.Clone();

        _inventory.AddItem(inventoryItem);
    }

    [Button]
    private void RemoveItem(InventoryItemConfig config)
    {
        _inventory.RemoveItem(config.item.Name);
    }
}
