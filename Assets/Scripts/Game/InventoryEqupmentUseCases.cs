using Game.GameEngine.Mechanics;

namespace Lessons.MetaGame.Inventory
{
    public static class InventoryEqupmentUseCases
    {
        public static void UnequipItem(InventoryItem inventoryItem, ListInventory listInventory, Equipment equipment)
        {
            var equipmentType = inventoryItem.GetComponent<Component_EquipType>().Type;
            
            var equippedItem = equipment.GetItemInSlot(equipmentType);
            if (equippedItem != null)
            {
                equipment.Unequip(equippedItem);
                listInventory.AddItem(equippedItem);
            }
        }

        public static void EquipItem(InventoryItem inventoryItem, ListInventory listInventory, Equipment equipment)
        {
            if (!equipment.CanEquip(inventoryItem))
                return;

            var equipmentType = inventoryItem.GetComponent<Component_EquipType>().Type;
            var equippedItem = equipment.GetItemInSlot(equipmentType);

            if (equippedItem == inventoryItem)
                return;

            if (equippedItem != null)
            {
                equipment.Unequip(equippedItem);
                listInventory.AddItem(equippedItem);
            }

            equipment.Equip(inventoryItem);
            listInventory.RemoveItem(inventoryItem);
        }
    }
}