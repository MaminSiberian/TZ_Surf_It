using System.Collections.Generic;

public static class InventorySaveManager
{
    private const string DATA_KEY = "InvData.json";

    public static void SaveInventoryData(List<ItemData> data)
    {
        JsonSaveSystem.SaveToFile(data, DATA_KEY);
    }
    public static List<ItemData> LoadInventoryData()
    {
        return JsonSaveSystem.LoadFromFile<List<ItemData>>(DATA_KEY);
    }
}
