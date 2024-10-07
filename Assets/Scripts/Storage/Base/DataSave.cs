using System.IO;
using UnityEngine;
using System.Threading.Tasks;

public sealed class DataSave : DataBase
{
    public async Task SaveAsync<T>(T data) where T : class, new()
    {
        var path = GetPath<T>();
        var json = JsonUtility.ToJson(data, true);
        await File.WriteAllTextAsync(path, json);
    }

    public void Save<T>(T data) where T: class, new()
    {
        var path = GetPath<T>();
        var json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }
}
