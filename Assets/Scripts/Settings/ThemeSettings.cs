using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz/ThemeSettings", fileName = "ThemeSettings", order = 1)]
public sealed class ThemeSettings : ScriptableObject
{
    private const int COUNT_STARS = 3;

    [field: SerializeField] public ThemeType Type { get; private set; }
    [field: SerializeField] public LevelsSettings Levels { get; private set; }

    public int GetTotalCountStars()
    {
        return Levels.LevelsSetting.Count * COUNT_STARS;
    }

    public List<LevelSettings> GetLevelByRank(UserRankType userRankType)
    {
        return Levels.LevelsSetting.Where(levelSetting => levelSetting.LevelRank == userRankType).ToList();
    }
}
