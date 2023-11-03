using System.Collections.Generic;

namespace Inventory
{
    public class InventoryModel
    {
        public Dictionary<UI.ItemModel, Slot> itemsInInventory = new Dictionary<UI.ItemModel, Slot>();
        
        public void AddItem(UI.ItemModel item, Slot slot)
        {
            itemsInInventory.Add(item, slot);
        }
        public void RemoveItem(UI.ItemModel item)
        {
            itemsInInventory.Remove(item);
        }
        public void ClearInventory()
        {
            itemsInInventory.Clear();
        }
    }
}
