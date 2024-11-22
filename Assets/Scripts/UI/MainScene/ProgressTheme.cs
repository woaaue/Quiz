using TMPro;
using Zenject;
using UnityEngine;
using UnityEngine.UI;
using JetBrains.Annotations;

public sealed class ProgressTheme : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private Image _themeImage;
    [SerializeField] private TextMeshProUGUI _themeName;
    [SerializeField] private TextMeshProUGUI _progressValue;

    private bool _isSetup;
    private ThemeType _type;
    private UserInfo _userInfo;
    private PopupService _popupService;
    private ThemeSettings _themesSettings;

    [Inject]
    public void Construct(UserInfo userInfo, PopupService popupService)
    {
        _userInfo = userInfo;
        _popupService = popupService;

        _userInfo.UserProgress.ProgressThemeChanged += OnProgressThemeChanged;
    }

    [UsedImplicitly]
    public void Click()
    {
        _popupService.ShowLevelsPopup(_type);
    }

    public void Setup(ThemeType themeType)
    {
        _type = themeType;
        _themesSettings = SettingsProvider.Get<ThemesSettings>().GetThemeSettings(_type);

        SetView();

        _isSetup = true;
    }

    private void OnEnable()
    {
        if (_isSetup) 
        {
            UpdateView();
        }
    }

    private void OnDestroy()
    {
        _userInfo.UserProgress.ProgressThemeChanged -= OnProgressThemeChanged;
    }

    private void UpdateView()
    {
        var totalCountStars = _themesSettings.GetTotalCountStars();
        var complete = _userInfo.UserProgress.GetStarsForTheme(_type);
        _progressValue.text = $"{complete}/{totalCountStars}";
        _fillImage.fillAmount = (float)complete / totalCountStars;
    }

    private void OnProgressThemeChanged(ThemeType themeType)
    {
        if (themeType == _type)
        {
            UpdateView();
        }
    }

    private void SetView()
    {
        if (_type == ThemeType.CSharp)
        {
            _themeName.text = "C#";
        }
        else
        {
            _themeName.text = _type.ToString();
        }

        _themeImage.sprite = SettingsProvider.Get<SpriteSettings>().GetThemeSprite(_type);

        UpdateView();
    }
}
