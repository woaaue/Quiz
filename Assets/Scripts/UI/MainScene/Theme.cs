using TMPro;
using Zenject;
using UnityEngine;
using UnityEngine.UI;
using JetBrains.Annotations;

public sealed class Theme : MonoBehaviour
{
    [SerializeField] private Image _imageTheme;
    [SerializeField] private GameObject _activeStar;
    [SerializeField] private GameObject _inactiveStar;
    [SerializeField] private TextMeshProUGUI _nameTheme;

    public ThemeType Type { get; private set; }

    [Inject] private UserInfo _userInfo;

    private bool _isFavourites;

    [UsedImplicitly]
    public void AddInFavourite()
    {
        _userInfo.UserData.AddFavouriteTheme(Type);
        _activeStar.SetActive(true);
    }

    [UsedImplicitly]
    public void RemoveInFavourite() 
    {
        _userInfo.UserData.RemoveFavouriteTheme(Type);
        _activeStar.SetActive(false);
    }

    public void Setup(ThemeType themeType, bool isFavourites)
    {
        Type = themeType;
        _isFavourites = isFavourites;

        FillTheme();
    }

    private void FillTheme()
    {
        _imageTheme.sprite = SettingsProvider.Get<SpriteSettings>().GetThemeSprite(Type);

        if (Type != ThemeType.CSharp)
        {
            _nameTheme.text = Type.ToString();
        }
        else
        {
            _nameTheme.text = "C#";
        }

        if (_isFavourites)
        {
            _activeStar.SetActive(false);
            _inactiveStar.SetActive(false);
        }
        else
        {
            _inactiveStar.gameObject.SetActive(true);

            if (_userInfo.UserData.FavouriteThemes.Contains(Type))
            {
                _activeStar.gameObject.SetActive(true);
            }
        }
    }
}
