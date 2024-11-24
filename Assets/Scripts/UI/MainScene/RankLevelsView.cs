using TMPro;
using Zenject;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
using System.Collections.Generic;

public sealed class RankLevelsView : MonoBehaviour
{
    private const string RANK_LOCALIZATION_PATTERN = "{0}_text";

    [SerializeField] private Image _filledProgress;
    [SerializeField] private RectTransform _levelContainer;
    [SerializeField] private TextMeshProUGUI _rankLevels;

    private UserInfo _userInfo;
    private ThemeType _themeType;
    private PoolService _poolService;
    private UserRankType _rankLevelType;

    [Inject]
    public void Construct(UserInfo userInfo, PoolService poolService)
    {
        _userInfo = userInfo;
        _poolService = poolService;
    }

    public void Setup(UserRankType userRankType, ThemeType themeType)
    {
        _themeType = themeType;
        _rankLevelType = userRankType;

        FillHeader();
        FillContent();
    }

    private void FillHeader()
    {
        _rankLevels.text = LocalizationProvider.GetText(LocalizationItemType.UI, string.Format(RANK_LOCALIZATION_PATTERN, _rankLevelType.ToString().ToLower()));
    }

    private void FillContent()
    {
        var countStarsLevels = 0;
        var totalCountStars = SettingsProvider.Get<ThemesSettings>().ThemeSettings[(int)_themeType].GetStarsLevel();
        var levelsForRank = SettingsProvider.Get<ThemesSettings>().GetThemeSettings(_themeType).GetLevelsByRank(_rankLevelType);
        var countStarsRank = levelsForRank.Count * totalCountStars;

        foreach (var level in levelsForRank) 
        {
            var levelObject = _poolService.Get<LevelView>();
            levelObject.transform.SetParent(_levelContainer, false);

            if (level.CheckReviewLevel())
            {
                var reviewLevel = level;
                var countStarsPreviousLevels = 0;
                var levelIndex = levelsForRank.IndexOf(level);
                var previousQuestions = new List<QuestionSettings>();

                for (int i = levelIndex - 1; i >= levelIndex - 3; i--)
                {
                    var previousLevel = levelsForRank[i];
                    countStarsPreviousLevels += _userInfo.UserProgress.GetCountStarsLevel(previousLevel.Id);

                    previousQuestions.AddRange(previousLevel.GetQuestions());
                }

                reviewLevel.SetQuestions(SetQuestionsSettings(previousQuestions, reviewLevel.GetMaxCountQuestions()));
                levelObject.Setup(reviewLevel, _themeType, countStarsPreviousLevels);
            }
            else
            {
                levelObject.Setup(level, _themeType);
            }

            countStarsLevels += _userInfo.UserProgress.GetCountStarsLevel(level.Id);

            List<QuestionSettings> SetQuestionsSettings(List<QuestionSettings> previousQuestions, int count)
            {
                var random = new Random();

                return previousQuestions
                    .OrderBy(_ => random.Next())
                    .Take(count)
                    .ToList();
            }
        }

        if (countStarsLevels != 0) 
        {
            _filledProgress.fillAmount = (float)countStarsLevels / countStarsRank;
        }
        else
        {
            _filledProgress.fillAmount = countStarsLevels;
        }
    }
}
