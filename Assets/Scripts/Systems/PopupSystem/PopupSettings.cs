using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Quiz/PopupSettings", fileName = "PopupSettings", order = 1)]
public sealed class PopupSettings : ScriptableObject
{
    [field: SerializeField] public List<PopupBase> Popups;

    public Popup<T> Get<T>() where T : PopupBaseSettings
    {
        try 
        {
            return (Popup<T>) Popups.First(popup => popup is Popup<T>);
        }
        catch 
        {
            throw new Exception($"There is no popup with these settings {typeof(T)}");
        }
    }
}
