using TMPro;
using Zenject;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using JetBrains.Annotations;

public sealed class LevelView : MonoBehaviour
{
    private const int DEFAULT_MAX_COUNT_STARS = 3;
    private const string PATTERN_TEXT_LEVEL = "{0} {1}";
    private const string PATTERN_VALUE_PROGRESS = "{0} / {1}";
    private const string LEVEL_TEXT_LOCALIZATION_KEY = "level_text";

    [SerializeField] private GameObject _locker;
    [SerializeField] private Image _filledProgress;
    [SerializeField] private TextMeshProUGUI _numberLevel;
    [SerializeField] private TextMeshProUGUI _progressStars;
    [SerializeField] private GameObject _requiredCountStars;
    [SerializeField] private TextMeshProUGUI _requiredCountStarsValue;

    private UserInfo _userInfo;
    private ThemeType _themeType;
    private PopupService _popupService;
    private LevelSettings _levelSettings;
    private int _countStarsPreviousLevels;

    [Inject]
    public void Construct(UserInfo userInfo, PopupService popupService)
    {
        _userInfo = userInfo;
        _popupService = popupService;
    }

    public void Setup(LevelSettings levelSettings, ThemeType themetype)
    {
        _levelSettings = levelSettings;
        _themeType = themetype;

        FillUserProgress();
    }

    public void Setup(LevelSettings levelSettings, ThemeType themetype, int countStarsPreviousLevels)
    {
        _levelSettings = levelSettings;
        _themeType = themetype;
        _countStarsPreviousLevels = countStarsPreviousLevels;

        FillUserProgress();
    }

    [UsedImplicitly]
    public void OpenLevel()
    {
        _popupService.ShowGamePopup(_levelSettings);
        _popupService.HideCurrentPopup();
    }

    private void FillUserProgress()
    {
        if (_userInfo.UserData.RankThemes.First(rankTheme => rankTheme.TypeTheme == _themeType).Rank < _levelSettings.LevelRank)
        {
            _locker.SetActive(true);
        }

        if (_levelSettings.Number > 3 && !_levelSettings.CheckReviewLevel())
        {
            if (!CheckNearestReviewLevelComplete())
            {
                _locker.SetActive(true);
            }
        }

        if (_levelSettings.CheckReviewLevel())
        {
            var requiredCountStars = _levelSettings.GetRequiredCountStars();

            if (_countStarsPreviousLevels < requiredCountStars)
            {
                _locker.SetActive(true);
                _requiredCountStars.SetActive(true);
                _requiredCountStarsValue.text = string.Format(PATTERN_VALUE_PROGRESS, _countStarsPreviousLevels, requiredCountStars);
            }
        }

        var count = _userInfo.UserProgress.GetCountStarsLevel(_levelSettings.Id);

        _filledProgress.fillAmount = (float)count / DEFAULT_MAX_COUNT_STARS;
        _progressStars.text = string.Format(PATTERN_VALUE_PROGRESS, count, DEFAULT_MAX_COUNT_STARS);
        _numberLevel.text = string.Format(PATTERN_TEXT_LEVEL, LocalizationProvider.GetText(LocalizationItemType.UI, LEVEL_TEXT_LOCALIZATION_KEY), _levelSettings.Number);
    }

    private bool CheckNearestReviewLevelComplete()
    {
        var residue = _levelSettings.Number % _levelSettings.GetNumberReviewLevelInterval();
        var reviewNumberLevel = _levelSettings.Number - residue;
        var reviewLevel = SettingsProvider.Get<ThemesSettings>().GetThemeSettings(_themeType).GetLevelForNumber(reviewNumberLevel);

        if (reviewLevel.LevelRank != _levelSettings.LevelRank)
        {
            return true;
        }

        if (_userInfo.UserProgress.GetCountStarsLevel(reviewLevel.Id) == 0)
        {
           return false;
        }

        return true;
    }
}
