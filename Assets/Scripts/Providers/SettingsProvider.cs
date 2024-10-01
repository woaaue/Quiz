using System;
using UnityEngine;
using System.Collections.Generic;

public static class SettingsProvider
{
    private const string PATH = "Settings/{0}";
    private static Dictionary<Type, ScriptableObject> _settings;

    static SettingsProvider()
    {
        _settings = new Dictionary<Type, ScriptableObject>();
    }

    public static T Get<T>() where T : ScriptableObject
    {
        var type = typeof(T);

        if (_settings.ContainsKey(type))
            return (T)_settings[type];

        _settings.Add(type, (T)Resources.Load(string.Format(PATH, type.Name)));

        return (T)_settings[type];
    }
}
