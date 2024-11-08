using TMPro;
using System;
using Zenject;
using UnityEngine;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;

public sealed class NamePopup : Popup<NamePopupSettings>
{
    private const string DEFAULT_NAME_KEY = "player_text";
    private const string DEFAULT_LANGUAGE_KEY = "{0}_popup_text";

    [SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private TMP_InputField _inputField;

    private UserInfo _userInfo;

    [Inject]
    public void Construct(UserInfo userInfo)
    {
        _userInfo = userInfo;
    }

    public override void Setup(NamePopupSettings settings)
    {
        base.Setup(settings);
    }

    [UsedImplicitly]
    public void ChangeLanguage()
    {
        LocalizationProvider.SwitchLanguage((LanguageType)_dropdown.value);
        FillDropdown();
    }

    [UsedImplicitly]
    public override void Close()
    {
        SetNickname();
        base.Close();
    }

    private void Start()
    {
        base.Start();
        FillDropdown();
    }

    private void FillDropdown()
    {
        var dropdownOptions = new List<TMP_Dropdown.OptionData>();
        var languageTypes = Enum.GetValues(typeof(LanguageType)).Cast<LanguageType>();

        foreach (var languageType in languageTypes)
        {
            var optionData = new TMP_Dropdown.OptionData($"{LocalizationProvider.GetText(LocalizationItemType.UI, string.Format(DEFAULT_LANGUAGE_KEY, languageType.ToString().ToLower()))}");

            dropdownOptions.Add(optionData);
        }

        _dropdown.options = dropdownOptions;
    }

    private void SetNickname()
    {
        if (_inputField.text != string.Empty)
        {
            _userInfo.UserProfile.ChangeName(_inputField.text);
        }
        else
        {
            _userInfo.UserProfile.ChangeName(LocalizationProvider.GetText(LocalizationItemType.UI, DEFAULT_NAME_KEY));
        }
    }
}

public sealed class NamePopupSettings : PopupBaseSettings
{

}
