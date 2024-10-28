using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public sealed class UserInfo
{
    [field: SerializeField] public UserData UserData { get; private set; }
    [field: SerializeField] public UserProfile UserProfile { get; private set; }
    [field: SerializeField] public UserProgress UserProgress { get; private set; }
    [field: SerializeField] public LanguageSettings LanguageSettings { get; private set; }

    public UserInfo() 
    {
        UserData = new UserData();
        UserProfile = new UserProfile();
        UserProgress = new UserProgress();
        LanguageSettings = new LanguageSettings();
    }
}

[Serializable]
public sealed class UserData
{
    private const int DEFAULT_COUNT_GOLD = 250;

    [field: SerializeField] public int Gold { get; private set; }
    [field: SerializeField] public List<ThemeType> FavouriteThemes { get; private set; }

    public event Action GoldChanged;

    public UserData()
    {
        Gold = DEFAULT_COUNT_GOLD;
        FavouriteThemes = new List<ThemeType>();
    }

    public void IncreaseValue(int value)
    {
        Gold += value;

        GoldChanged?.Invoke();
    }

    public bool TryDecreaseValue(int value) 
    {
        if (Gold - value < 0)
            return false;

        Gold -= value;

        GoldChanged?.Invoke();

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
}

[Serializable]
public sealed class UserProfile
{
    private const string DEFAULT_NAME = "";

    [field: SerializeField] public string UserName { get; private set; }
    [field: SerializeField] public int CountExecution { get; private set; }
    [field: SerializeField] public UserRankType UserRank { get; private set; }

    public event Action UserNameChanged;

    public UserProfile()
    {
        UserName = DEFAULT_NAME;
    }

    public void ChangeCountExecution()
    {
        CountExecution++;
    }

    public void ChangeName(string name)
    {
        UserName = name;

        UserNameChanged?.Invoke();
    }

    public void ChangeRank(UserRankType rankType) 
    {
        UserRank = rankType;
    }
}