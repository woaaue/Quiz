using System.IO;
using UnityEngine;

public abstract class DataBase
{ 
    protected string GetPath<T>() where T : class
    {
        return Path.Combine(Application.persistentDataPath, typeof(T).Name);
    }
}
