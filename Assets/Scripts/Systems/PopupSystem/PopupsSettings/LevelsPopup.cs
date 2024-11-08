using TMPro;
using System;
using Zenject;
using UnityEngine;
using System.Linq;

public sealed class LevelsPopup : Popup<LevelsPopupSettings>
{
    [SerializeField] private Transform _levelsContainer;
    [SerializeField] private TextMeshProUGUI _themeName;

    private PoolService _poolService;
    private ThemeType _levelsTheme;

    [Inject]
    public void Construct(PoolService poolService)
    {
        _poolService = poolService;

        FillContent();
    }

    public override void Setup(LevelsPopupSettings settings)
    {
        _levelsTheme = settings.CurrentTheme;
    }

    private void FillContent()
    {
        var rankTypes = Enum.GetValues(typeof(UserRankType)).Cast<UserRankType>();

        foreach (var rankType in rankTypes)
        {
            var levelsObject = _poolService.Get<RankLevelsView>();

            levelsObject.transform.SetParent(_levelsContainer, false);
            levelsObject.Setup(rankType, _levelsTheme);
        }
    }
}

public sealed class LevelsPopupSettings : PopupBaseSettings
{
    public ThemeType CurrentTheme { get; private set; }

    public LevelsPopupSettings(ThemeType currentTheme)
    {
        CurrentTheme = currentTheme;
    }
}
