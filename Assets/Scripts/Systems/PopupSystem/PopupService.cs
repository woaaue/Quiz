using Zenject;
using UnityEngine;

public sealed class PopupService : MonoBehaviour
{
    [SerializeField] private PopupQueueController _popupController;

    [Inject] private UserInfo _userInfo;

    public void ShowStartPopup()
    {
        //_popupController.AddPopup(new StartPopupSettings());
    }

    private void Start()
    {
        if (_userInfo.UserProfile.CountExecution < 2)
        {
            ShowStartPopup();
        }
    }
}
