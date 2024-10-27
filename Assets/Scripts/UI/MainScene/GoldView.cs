using TMPro;
using Zenject;
using UnityEngine;

public sealed class GoldView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentValue;

    [Inject] private UserInfo _userInfo;

    private void Start()
    {
        _currentValue.text = _userInfo.UserData.Gold.ToString();

        EventSystem.Subscribe<ChangeGoldEvent>(OnChangeGold);
    }

    private void OnDestroy()
    {
        EventSystem.Unsubscribe<ChangeGoldEvent>(OnChangeGold);
    }

    private void OnChangeGold(ChangeGoldEvent goldEvent)
    {
        _currentValue.text = goldEvent.Gold.ToString();
    }
}
