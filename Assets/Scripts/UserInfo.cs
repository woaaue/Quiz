using System;
using Unity.VisualScripting;
using System.Collections.Generic;

public sealed class UserInfo : IInitializable
{
    public UserData UserData { get; private set; }
    public UserProgress UserProgress { get; private set; }

    public void Initialize()
    {
        UserData = new UserData();
        UserProgress = new UserProgress();

        // TODO: load saves
    }
}

[Serializable]
public sealed class UserData
{
    public event Action<int> ValueChanged;

    public int Gold { get; private set; }

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
    public Dictionary<ThemeType, List<LevelProgress>> LevelsProgress { get; private set; }

    public UserProgress()
    {
        LevelsProgress = new Dictionary<ThemeType, List<LevelProgress>>();
    }

    public sealed class LevelProgress
    {
        public int Level { get; private set; }
        public bool IsPassed { get; private set; }
        public int CountStars { get; private set; }
    }
}
