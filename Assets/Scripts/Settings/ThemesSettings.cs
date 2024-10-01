using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Quiz/ThemesSettings", fileName = "ThemesSettings", order = 1)]
public sealed class ThemesSettings : ScriptableObject
{
    [field: SerializeField] public List<ThemeSettings> ThemeSettings { get; private set; }
}
