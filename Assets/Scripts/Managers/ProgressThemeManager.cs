using System;
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
        foreach (ThemeType theme in Enum.GetValues(typeof(ThemeType)))
        {
            var prefabObject = Instantiate(_prefab, _container, false);

            prefabObject.Setup(theme);
        }
    }
}
