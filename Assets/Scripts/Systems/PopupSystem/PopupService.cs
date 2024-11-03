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

    private void Start()
    {
        if (_userInfo.UserProfile.CountExecution < 2)
        {
            ShowStartPopups();
        }
    }
}
