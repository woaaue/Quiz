using System;
using Zenject;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;

public sealed class ThemeManager : MonoBehaviour
{
    [SerializeField] private bool _isFavourites;
    [SerializeField] private Transform _container;
    [SerializeField] private GridLayoutGroup _layoutGroup;

    private bool _isInit;
    private UserInfo _userInfo;
    private PoolService _poolService;
    private List<Theme> _favouriteThemes;

    [Inject]
    public void Construct(UserInfo userInfo, PoolService poolService)
    {
        _userInfo = userInfo;
        _poolService = poolService;
    }

    private void Start()
    {
        FillContent();
        _isInit = true;
    }

    private void OnEnable()
    {
        if (_isFavourites && _isInit)
        {
            CheckFavouriteThemes();   
        }
    }

    private void FillContent()
    {
        if (_isFavourites)
        {
            _favouriteThemes = new List<Theme>();

            if (_userInfo.UserData.FavouriteThemes.Count == 0)
            {
                return;
            }
            
            foreach (var themeType in _userInfo.UserData.FavouriteThemes)
            {
                var element = _poolService.Get<Theme>();

                element.transform.SetParent(_container, false);
                element.Setup(themeType, _isFavourites);
                _favouriteThemes.Add(element);
            }

            SetGridSettings(_favouriteThemes.Count);
        }
        else
        {
            var themeTypes = Enum.GetValues(typeof(ThemeType)).Cast<ThemeType>();

            foreach (var themeType in themeTypes)
            {
                var element = _poolService.Get<Theme>();

                element.transform.SetParent(_container, false);
                element.Setup(themeType, _isFavourites);
            }
        }
    }

    private void CheckFavouriteThemes()
    {   
        foreach (var themeType in _userInfo.UserData.FavouriteThemes)
        {
            if (!_favouriteThemes.Any(favouriteTheme => favouriteTheme.Type == themeType))
            {
                var element = _poolService.Get<Theme>();

                element.transform.SetParent(_container, false);
                element.Setup(themeType, _isFavourites);
                _favouriteThemes.Add(element);
            }
            else
            {
                var inactiveTheme = _favouriteThemes.First(favouriteTheme => favouriteTheme.Type == themeType);

                if (!inactiveTheme.gameObject.activeInHierarchy)
                {
                    inactiveTheme.gameObject.SetActive(true);
                }
            }
        }

        foreach (var favouriteTheme in _favouriteThemes)
        {
            if (!_userInfo.UserData.FavouriteThemes.Contains(favouriteTheme.Type))
            {
                favouriteTheme.gameObject.SetActive(false);
            }
        }

        SetGridSettings(_favouriteThemes.Count(favouriteTheme => favouriteTheme.gameObject.activeInHierarchy));
    }

    private void SetGridSettings(int countElement)
    {
        if (countElement < 2)
        {
            _layoutGroup.childAlignment = TextAnchor.UpperLeft;
        }
        else 
        {
            _layoutGroup.childAlignment = TextAnchor.UpperCenter;
        }
    }
}
