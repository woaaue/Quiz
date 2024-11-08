using System;
using Zenject;
using UnityEngine;
using System.Linq;

public sealed class RankPopup : Popup<RankPopupSettings>
{
    [SerializeField] private Transform _container;

    private PoolService _poolService;

    [Inject]
    public void Construct(PoolService poolService)
    {
        _poolService = poolService;
    }

    private void Start()
    {
        base.Start();
        FillRankSelectors();
    }

    private void FillRankSelectors()
    {
        var themeTypes = Enum.GetValues(typeof(ThemeType)).Cast<ThemeType>();

        foreach (var themeType in themeTypes)
        {
            var rankSelectorPrefab = _poolService.Get<RankSelector>();

            rankSelectorPrefab.transform.SetParent(_container, false);
            rankSelectorPrefab.Setup(themeType);
        }
    }
}

public sealed class RankPopupSettings : PopupBaseSettings
{

}
