using System;
using System.Collections.Generic;

public static class EventSystem
{
    private static Dictionary<string, List<object>> _signalCallbacks;

    static EventSystem()
    {
        _signalCallbacks = new Dictionary<string, List<object>>();
    }

    public static void Subscribe<T>(Action<T> callback)
    {
        var key = typeof(T).Name;

        if (_signalCallbacks.ContainsKey(key))
        {
            _signalCallbacks[key].Add(callback);
        }
        else
        {
            _signalCallbacks.Add(key, new List<object>() {callback});
        }
    }

    public static void Unsubscribe<T>(Action<T> callback) 
    {
        var key = typeof(T).Name;

        if ( _signalCallbacks.ContainsKey(key))
        {
            _signalCallbacks[key].Remove(callback);
        }
    }

    public static void Invoke<T>(T signal)
    {
        var key = typeof(T).Name;

        foreach (var obj in _signalCallbacks[key])
        {
            var callback = obj as Action<T>;

            callback?.Invoke(signal);
        }
    }
}
