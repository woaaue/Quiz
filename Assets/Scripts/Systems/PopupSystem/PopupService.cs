using Zenject;
using UnityEngine;

public sealed class PopupService : MonoBehaviour
{
    [SerializeField] private PopupQueueController _popupController;

    [Inject] private UserInfo _userInfo;

    public void ShowStartPopups()
    {
        _popupController.AddPopup(new NamePopupSettings());
        _popupController.AddPopup(new RankPopupSettings());
    }

    public void ShowLevelsPopup(ThemeType theme)
    {
        _popupController.AddPopup(new LevelsPopupSettings(theme));
    }

    public void HidePopups()
    {
        _popupController.HideAllPopups();
    }

    private void Start()
    {
        if (_userInfo.UserProfile.CountExecution < 2)
        {
            ShowStartPopups();
        }
    }
}
