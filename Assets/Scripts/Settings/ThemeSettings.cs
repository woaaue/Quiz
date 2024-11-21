using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Quiz/ThemeSettings", fileName = "ThemeSettings", order = 1)]
public sealed class ThemeSettings : ScriptableObject
{
    private const int COUNT_STARS = 3;

    [field: SerializeField] public ThemeType Type { get; private set; }
    [field: SerializeField] public LevelsSettings Levels { get; private set; }

    public LevelSettings GetLevelForId(string id)
    {
        return Levels.LevelsSetting.First(levelSetting => levelSetting.Id == id);
    }

    public int GetTotalCountStars()
    {
        return Levels.LevelsSetting.Count * COUNT_STARS;
    }

    public int GetStarsLevel()
    {
        return COUNT_STARS;
    }

    public List<LevelSettings> GetLevelsByRank(UserRankType userRankType)
    {
        return Levels.LevelsSetting.Where(levelSetting => levelSetting.LevelRank == userRankType).ToList();
    }
}
