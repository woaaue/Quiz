using UnityEngine;

[CreateAssetMenu(menuName = "Quiz/ThemeSettings", fileName = "ThemeSettings", order = 1)]
public sealed class ThemeSettings : ScriptableObject
{
    [field: SerializeField] public ThemeType Type { get; private set; }
    [field: SerializeField] public LevelsSettings Levels { get; private set; }
}
