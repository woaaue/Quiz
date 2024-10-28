using TMPro;
using Zenject;
using UnityEngine;

public sealed class GoldView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentValue;

    [Inject] private UserInfo _userInfo;

    private void Start()
    {
        OnChangeGold();

        _userInfo.UserData.GoldChanged += OnChangeGold;
    }

    private void OnDestroy()
    {
        _userInfo.UserData.GoldChanged -= OnChangeGold;
    }

    private void OnChangeGold()
    {
        _currentValue.text = _userInfo.UserData.Gold.ToString();
    }
}
