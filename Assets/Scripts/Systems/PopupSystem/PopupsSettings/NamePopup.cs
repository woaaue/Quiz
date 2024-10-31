using TMPro;
using Zenject;
using UnityEngine;
using JetBrains.Annotations;

public class NamePopup : Popup<NamePopupSettings>
{
    private const string DEFAULT_NAME_KEY = "player_text";

    [SerializeField] private TMP_InputField _inputField;

    [Inject] private UserInfo _userInfo;

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
    public override void Close()
    {
        SetNickname();
        base.Close();
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
