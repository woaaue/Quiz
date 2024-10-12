using System.IO;
using System.Threading.Tasks;
using Unity.Plastic.Newtonsoft.Json;

public sealed class DataSave : DataBase
{
    public async Task SaveAsync<T>(T data) where T : class, new()
    {
        var path = GetPath<T>();
        var json = JsonConvert.SerializeObject(data);
        await File.WriteAllTextAsync(path, json);
    }

    public void Save<T>(T data) where T: class, new()
    {
        var path = GetPath<T>();
        var json = JsonConvert.SerializeObject(data);
        File.WriteAllText(path, json);
    }
}
