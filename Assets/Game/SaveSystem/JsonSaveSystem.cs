using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class JsonSaveSystem
{
    public static void SaveToFile(object data, string key)
    {
        string path = BuildPath(key);
        string json = JsonConvert.SerializeObject(data);

        using (var fileStream = new StreamWriter(path))
        {
            fileStream.Write(json);
        }
    }
    public static T LoadFromFile<T>(string key)
    {
        string path = BuildPath(key);
        if (!File.Exists(path))
        {
            File.Create(path);
        }

        using (var fileStream = new StreamReader(path))
        {
            var json = fileStream.ReadToEnd();

            var obj = JsonConvert.DeserializeObject<T>(json);

            return obj;
        }
    }
    private static string BuildPath(string key)
    {
        return Path.Combine(Application.persistentDataPath, key);
    }
}
