using TMPro;
using System;
using Zenject;
using UnityEngine;
using JetBrains.Annotations;

public class NamePopup : Popup<NamePopupSettings>
{
    private const string DEFAULT_NAME_KEY = "player_text";

    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private GameObject _languageScroll;
    [SerializeField] private TextMeshProUGUI _currentLanguage;
    [SerializeField] private Transform _languageScrollContainer;

    private bool _isSwitch;
    private UserInfo _userInfo;
    private PoolService _poolService;

    [Inject]
    public void Construct(PoolService poolService, UserInfo userInfo)
    {
        _userInfo = userInfo;
        _poolService = poolService;

        FillLanguageScroll();
    }

    public override void Setup(NamePopupSettings settings)
    {
        base.Setup(settings);
    }

    [UsedImplicitly]
    public void SetRank(int userRankType)
    {
        _userInfo.UserProfile.ChangeRank((UserRankType)userRankType);
    }

    [UsedImplicitly]
    public void SwichSelectLanguage()
    {
        _isSwitch = !_isSwitch;
        _languageScroll.SetActive(_isSwitch);
    }

    [UsedImplicitly]
    public override void Close()
    {
        SetNickname();
        base.Close();
    }

    private void Start()
    {
        LocalizationProvider.LanguageChanged += OnChangeCurrentLanguage;
        OnChangeCurrentLanguage();
        base.Start();
    }

    private void OnDestroy()
    {
        LocalizationProvider.LanguageChanged -= OnChangeCurrentLanguage;
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

    private void OnChangeCurrentLanguage()
    {
        _currentLanguage.text = LocalizationProvider.CurrentLanguage.ToString();
    }

    private void FillLanguageScroll()
    {
        foreach (LanguageType type in Enum.GetValues(typeof(LanguageType)))
        {
            var element = _poolService.Get<Language>();

            element.transform.SetParent(_languageScrollContainer, false);
            element.Setup(type);
            element.gameObject.SetActive(true);
        }
    }
}

public sealed class NamePopupSettings : PopupBaseSettings
{

}
