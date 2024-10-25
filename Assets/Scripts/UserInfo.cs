using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public sealed class UserInfo
{
    [field: SerializeField] public string UserName;
    [field: SerializeField] public UserData UserData { get; private set; }
    [field: SerializeField] public UserProgress UserProgress { get; private set; }
    [field: SerializeField] public LanguageSettings LanguageSettings { get; private set; }

    public UserInfo() 
    {
        UserData = new UserData();
        UserProgress = new UserProgress();
        LanguageSettings = new LanguageSettings();
    }
}

[Serializable]
public sealed class UserData
{
    public event Action<int> ValueChanged;

    [field: SerializeField] public int Gold { get; private set; }
    [field: SerializeField] public List<ThemeType> FavouriteThemes { get; private set; }

    public UserData()
    {
        Gold = 250;
        FavouriteThemes = new List<ThemeType>();
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

    public void AddFavouriteTheme(ThemeType type)
    {
        FavouriteThemes.Add(type);
    }

    public void RemoveFavouriteTheme(ThemeType type)
    {
        FavouriteThemes.Remove(type);
    }
}

[Serializable]
public sealed class UserProgress
{
    [field: SerializeField] public List<LevelProgress> Progresses { get; private set; }

    public UserProgress()
    {
        Progresses = new List<LevelProgress>();
    }

    public int GetStarsForTheme(ThemeType themeType)
    {
        return Progresses
            .Where(progress => progress.ThemeType == themeType)
            .Sum(progress => progress.CountStars);
    }

    public sealed class LevelProgress
    {
        public ThemeType ThemeType;
        public string Id;
        public int CountStars;
        public bool IsPassed => CountStars > 0;
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
