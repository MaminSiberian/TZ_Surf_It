using System.Collections.Generic;

public static class InventorySaveManager
{
    private const string DATA_KEY = "InvData.json";

    public static void SaveInventoryData(Dictionary<UI.ItemModel, Inventory.Slot> data)
    {
        JsonSaveSystem.SaveToFile(data, DATA_KEY);
    }
    public static Dictionary<UI.ItemModel, Inventory.Slot> LoadInventoryData()
    {
        return JsonSaveSystem.LoadFromFile<Dictionary<UI.ItemModel, Inventory.Slot>>(DATA_KEY);
    }
}
