using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryModel
    {
        public Dictionary<UI.ItemModel, Vector2> itemsInInventory = new Dictionary<UI.ItemModel, Vector2>();
        
        public void AddItem(UI.ItemModel item, Vector2 position)
        {
            itemsInInventory.Add(item, position);
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
