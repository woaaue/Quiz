using UnityEngine;
using System.Linq;
using NaughtyAttributes;
using System.Collections.Generic;
using System;

[CreateAssetMenu(menuName = "Quiz/ThemesSettings", fileName = "ThemesSettings", order = 1)]
public sealed class ThemesSettings : ScriptableObject
{
    [field: SerializeField] public List<ThemeSettings> ThemeSettings { get; private set; }

    public ThemeSettings GetThemeSettings(ThemeType themeType)
    {
        return ThemeSettings
            .First(themeSetting => themeSetting.Type == themeType);
    }

    public ThemeType GetThemeTypeForIdLevel(string id)
    {
        foreach (var themeSettings in ThemeSettings) 
        {
            foreach (var level in themeSettings.Levels.LevelsSetting)
            {
                if (level.Id == id)
                {
                    return themeSettings.Type;
                }
            }
        }

        throw new Exception($"There is no such level {id}");
    }

#if UNITY_EDITOR

    [Button("Fill default level settings")]
    private void GenerateData()
    {
        ThemeSettings.ForEach(themeSettings =>
        {
            themeSettings.Levels.FillSettings();
        });
    }

    [Button("SaveAssets")]
    private void Save()
    {
        foreach (var themeSetting in ThemeSettings) 
        {
            foreach (var level in themeSetting.Levels.LevelsSetting)
            {
                level.SaveAssets();
            }
        }
    }

#endif
}
