using TMPro;
using System;
using Zenject;
using UnityEngine;
using UnityEngine.UI;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;

public sealed class RankSelector : MonoBehaviour
{
    private const string PATTERN_RANK_TYPE_LOCALIZATION_KEY = "{0}_text";

    [SerializeField] private Image _themeImage;
    [SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private TextMeshProUGUI _nameTheme;

    private UserInfo _userInfo;
    private ThemeType _themeType;

    [Inject]
    public void Construct(UserInfo userInfo)
    {
        _userInfo = userInfo;
    }

    [UsedImplicitly]
    public void ChangeSettingsUserTheme()
    {
        _userInfo.UserData.ChangeRankTheme(_themeType, (UserRankType)_dropdown.value);
    }

    public void Setup(ThemeType themeType)
    {
        _themeType = themeType;

        FillThemeData();
        FillDropdown();
    }

    private void FillDropdown()
    {
        var dropdownOptionsData = new List<TMP_Dropdown.OptionData>();
        var rankTypes = Enum.GetValues(typeof(UserRankType)).Cast<UserRankType>();

        foreach (var rankType in rankTypes)
        {
            var optionData = new TMP_Dropdown.OptionData($"{LocalizationProvider.GetText(LocalizationItemType.UI, string.Format(PATTERN_RANK_TYPE_LOCALIZATION_KEY, rankType.ToString().ToLower()))}");
            
            dropdownOptionsData.Add(optionData);
        }

        _dropdown.AddOptions(dropdownOptionsData);
    }

    private void FillThemeData()
    {
        _themeImage.sprite = SettingsProvider.Get<SpriteSettings>().GetThemeSprite(_themeType);

        if (_themeType == ThemeType.CSharp)
        {
            _nameTheme.text = "C#";
        }
        else
        {
            _nameTheme.text = _themeType.ToString();
        }
    }
}
