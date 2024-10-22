using TMPro;
using Zenject;
using UnityEngine;
using UnityEngine.UI;

public sealed class Theme : MonoBehaviour
{
    [SerializeField] private GameObject _star;
    [SerializeField] private Image _imageTheme;
    [SerializeField] private TextMeshProUGUI _nameTheme;

    [Inject] UserInfo _userInfo;

    private ThemeType _type;

    public void Setup(ThemeType themeType)
    {
        _type = themeType;
    }
}
