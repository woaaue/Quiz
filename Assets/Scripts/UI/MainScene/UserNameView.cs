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
        _welcomeText.text = string.Format(PATTERN, LocalizationProvider.GetText(LocalizationItemType.UI, KEY_ZERO), _userInfo.UserProfile.UserName);

        EventSystem.Subscribe<ChangeNameEvent>(OnChangeName);
    }

    private void OnDestroy()
    {
        EventSystem.Unsubscribe<ChangeNameEvent>(OnChangeName);
    }

    private void OnChangeName(ChangeNameEvent nameEvent)
    {
        _welcomeText.text = string.Format(PATTERN, LocalizationProvider.GetText(LocalizationItemType.UI, KEY_ZERO), nameEvent.Name);
    }
}
