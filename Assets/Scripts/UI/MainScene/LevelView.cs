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

    private UserInfo _userInfo;
    private ThemeType _themeType;
    private PopupService _popupService;
    private LevelSettings _levelSettings;

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

        var count = _userInfo.UserProgress.GetCountStarsLevel(_levelSettings.Id);

        _filledProgress.fillAmount = (float)count / DEFAULT_MAX_COUNT_STARS;
        _progressStars.text = string.Format(PATTERN_VALUE_PROGRESS, count, DEFAULT_MAX_COUNT_STARS);
        _numberLevel.text = string.Format(PATTERN_TEXT_LEVEL, LocalizationProvider.GetText(LocalizationItemType.UI, LEVEL_TEXT_LOCALIZATION_KEY), _levelSettings.Number);
    }
}
