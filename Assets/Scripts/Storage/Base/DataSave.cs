using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;

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
