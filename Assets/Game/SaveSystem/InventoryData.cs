using System.Collections.Generic;

[System.Serializable]
public class InventoryData
{
    public Dictionary<UI.ItemModel, Inventory.Slot> itemsInInventory;
}
