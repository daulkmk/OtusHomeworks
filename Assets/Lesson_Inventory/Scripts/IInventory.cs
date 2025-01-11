using System.Collections.Generic;

namespace Lessons.MetaGame.Inventory
{
    public interface IInventory
    {
        List<InventoryItem> GetItems();

        void AddItem(InventoryItem item);
        
        void RemoveItem(InventoryItem item);
        void RemoveItem(string itemName);
        void RemoveItems(string name, int count);

        void AddObserver(IInventoryObserver observer);
        void RemoveObserver(IInventoryObserver observer);

        int GetCount(string item);
    }
}