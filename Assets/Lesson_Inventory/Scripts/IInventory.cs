using System.Collections.Generic;

namespace Lessons.MetaGame.Inventory
{
    public interface IInventory
    {
        List<InventoryItem> GetItems();

        void AddItem(InventoryItem item);
        void RemoveItem(InventoryItem item);
        void RemoveItem(string itemName);

        void AddObserver(IInventoryObserver observer);
        void RemoveObserver(IInventoryObserver observer);
    }
}