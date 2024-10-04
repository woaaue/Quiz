using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Quiz/ThemesSettings", fileName = "ThemesSettings", order = 1)]
public sealed class ThemesSettings : ScriptableObject
{
    [field: SerializeField] public List<ThemeSettings> ThemeSettings { get; private set; }

#if UNITY_EDITOR

    [Button("Generate ID levels and question settings")]
    private void GenerateData()
    {
        ThemeSettings.ForEach(themeSettings =>
        {
            themeSettings.FillQuestions();
            themeSettings.Levels.GenerateId();
        });
    }

#endif
}
