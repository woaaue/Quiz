using System;
using System.Linq;
using UnityEngine;

public sealed class ProgressThemeManager : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private ProgressTheme _prefab;

    private void Start()
    {
        InitProgress();
    }

    private void InitProgress()
    {
        var themeTypes = Enum.GetValues(typeof(ThemeType)).Cast<ThemeType>();

        foreach (var theme in themeTypes)
        {
            var prefabObject = Instantiate(_prefab, _container, false);

            prefabObject.Setup(theme);
        }
    }
}
