using TMPro;
using Zenject;
using UnityEngine;
using UnityEngine.UI;

public sealed class ProgressTheme : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private Image _themeImage;
    [SerializeField] private TextMeshProUGUI _themeName;
    [SerializeField] private TextMeshProUGUI _progressValue;

    [Inject] private UserInfo _userInfo;

    private bool _isSetup;
    private ThemeType _type;
    private ThemeSettings _themesSettings;

    public void Setup(ThemeType themeType)
    {
        _type = themeType;
        _themesSettings = SettingsProvider.Get<ThemesSettings>().GetThemeSettings(_type);

        SetView();
    }

    private void OnEnable()
    {
        if (_isSetup) 
        {
            UpdateView();
        }
    }

    private void UpdateView()
    {
        var totalCountStars = _themesSettings.GetTotalCountStars();
        var complete = _userInfo.UserProgress.GetStarsForTheme(_type);
        _progressValue.text = $"{complete}/{totalCountStars}";
        _fillImage.fillAmount = complete / totalCountStars;
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
        _isSetup = true;

        UpdateView();
    }
}
