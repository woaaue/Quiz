using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;

public sealed class DataLoad : DataBase
{
    public async Task<T> Load<T>() where T : class, new()
    {
        var path = GetPath<T>();

        if (!File.Exists(path))
        {
            return new T();
        }

        var json = await File.ReadAllTextAsync(path);
        
        return JsonConvert.DeserializeObject<T>(json);
    }
}
