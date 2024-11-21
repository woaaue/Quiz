using TMPro;
using Zenject;
using UnityEngine;
using static UserProgress;
using JetBrains.Annotations;

public sealed class ResultPopup : Popup<ResultPopupSettings>
{
    private const int MODIFIER_COIN = 10;
    private const int COUNT_QUESTION = 5;
    private const string PATTERN_CORRECT_TEXT = "{0}/{1} {2}";
    private const string LOCALIZATION_KEY_WIN = "win_text";
    private const string LOCALIZATION_KEY_LOSE = "lose_text";
    private const string LOCALIZATION_KEY_CORRECT = "correct_text";

    [SerializeField] private TextMeshProUGUI _result;
    [SerializeField] private TextMeshProUGUI _countStarsValue;
    [SerializeField] private TextMeshProUGUI _countReceivedMoneyValue;
    [SerializeField] private TextMeshProUGUI _countCorrectAnswersValue;
    [SerializeField] private GameObject _levelsButton;
    [SerializeField] private GameObject _playAgainButton;

    private int _countStars;
    private int _countReceivedMoney;
    private int _countCorrectAnswers;

    private UserInfo _userInfo;
    private ThemeType _themeType;
    private PopupService _popupService;
    private LevelProgress _levelProgress;
    private ThemesSettings _themesSettings;

    [Inject]
    public void Construct(UserInfo userInfo, PopupService popupService)
    {
        _userInfo = userInfo;
        _popupService = popupService;
    }

    public override void Setup(ResultPopupSettings settings)
    {
        _countCorrectAnswers = settings.CountCorrectAnswers;
        _countReceivedMoney = _countCorrectAnswers * MODIFIER_COIN;
        _themesSettings = SettingsProvider.Get<ThemesSettings>();
        _countStars = CalculateStars();
        _themeType = _themesSettings.GetThemeTypeForIdLevel(settings.LevelId);

        FillView();
        _userInfo.UserData.IncreaseValue(_countReceivedMoney);
        FillLevelProgress(settings.LevelId);
        _userInfo.UserProgress.AddLevel(_levelProgress);
        base.Setup(settings);
    }

    [UsedImplicitly]
    public void PlayAgain()
    {
        _popupService.ShowGamePopup(_themesSettings.ThemeSettings[(int)_themeType].GetLevelForId(_levelProgress.Id));
        Close();
    }

    [UsedImplicitly]
    public void Levels()
    {
        _popupService.ShowLevelsPopup(_themeType);
        Close();
    }

    private void FillView()
    {
        if (_countStars < 1)
        {
            _result.text = LocalizationProvider.GetText(LocalizationItemType.UI, LOCALIZATION_KEY_LOSE);
        }
        else
        {
            _result.text = LocalizationProvider.GetText(LocalizationItemType.UI, LOCALIZATION_KEY_WIN);
            
        }

        _countStarsValue.text = $"+{_countStars}";
        _countReceivedMoneyValue.text = $"+{_countReceivedMoney}";
        _countCorrectAnswersValue.text = string.Format(PATTERN_CORRECT_TEXT, _countCorrectAnswers, COUNT_QUESTION, LocalizationProvider.GetText(LocalizationItemType.UI, LOCALIZATION_KEY_CORRECT));
    }

    private void FillLevelProgress(string id)
    {
        _levelProgress = new LevelProgress();
        _levelProgress.Id = id;
        _levelProgress.ThemeType = _themeType;
        _levelProgress.CountStars = _countStars;
    }

    private int CalculateStars()
    {
        float percentage = (float)_countCorrectAnswers / COUNT_QUESTION;

        if (percentage == 1f)
        {
            return 3;
        }

        if (percentage >= 0.6f)
        {
            return 2;
        }

        if (percentage >= 0.2f)
        {
            return 1;
        }

        return 0;
    }
}

public sealed class ResultPopupSettings : PopupBaseSettings
{
    public string LevelId { get; private set; }
    public int CountCorrectAnswers { get; private set; }

    public ResultPopupSettings(string levelId, int countCorrectAnswers)
    {
        LevelId = levelId;
        CountCorrectAnswers = countCorrectAnswers;
    }
}
