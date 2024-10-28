using TMPro;
using Zenject;
using UnityEngine;

public sealed class UserNameView : MonoBehaviour
{
    private const string PATTERN = "{0}, {1}!";
    private const string KEY_ZERO = "weclome_text";

    [SerializeField] private TextMeshProUGUI _welcomeText;

    [Inject] private UserInfo _userInfo;

    private void Start()
    {
        OnChangeName();

        _userInfo.UserProfile.UserNameChanged += OnChangeName;
    }

    private void OnDestroy()
    {
        _userInfo.UserProfile.UserNameChanged -= OnChangeName;
    }

    private void OnChangeName()
    {
        _welcomeText.text = string.Format(PATTERN, LocalizationProvider.GetText(LocalizationItemType.UI, KEY_ZERO), _userInfo.UserProfile.UserName);
    }
}
