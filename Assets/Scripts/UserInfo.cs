using System;
using UnityEngine;
using Unity.VisualScripting;
using System.Collections.Generic;

public sealed class UserInfo : IInitializable
{
    [field: SerializeField] public UserData UserData { get; private set; }
    [field: SerializeField] public UserProgress UserProgress { get; private set; }
    [field: SerializeField] public LanguageSettings LanguageSettings { get; private set; }

    public void Initialize()
    {
        UserData = new UserData();
        UserProgress = new UserProgress();
        LanguageSettings = new LanguageSettings();

        // TODO: load saves
    }
}

[Serializable]
public sealed class UserData
{
    public event Action<int> ValueChanged;

    [field: SerializeField] public int Gold { get; private set; }

    public UserData()
    {
        Gold = 250;
    }

    public void IncreaseValue(int value)
    {
        Gold += value;

        ValueChanged?.Invoke(Gold);
    }

    public bool TryDecreaseValue(int value) 
    {
        if (Gold - value < 0)
            return false;

        Gold -= value;

        ValueChanged?.Invoke(Gold);

        return true;
    }
}

[Serializable]
public sealed class UserProgress
{
    [field: SerializeField] public Dictionary<ThemeType, List<LevelProgress>> LevelsProgress { get; private set; }

    public UserProgress()
    {
        LevelsProgress = new Dictionary<ThemeType, List<LevelProgress>>();
    }

    public sealed class LevelProgress
    {
        public string Id;
        public bool IsPassed => CountStars > 0;
        public int CountStars;
    }
}

[Serializable]
public sealed class LanguageSettings
{
    [field: SerializeField] public LanguageType SaveLanguage { get; private set; }

    public LanguageSettings() 
    {
        SaveLanguage = LanguageType.Ru;
    }
}
