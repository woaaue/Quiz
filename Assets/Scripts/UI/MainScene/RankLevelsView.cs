using TMPro;
using Zenject;
using UnityEngine;
using UnityEngine.UI;

public sealed class RankLevelsView : MonoBehaviour
{
    private const string RANK_LOCALIZATION_PATTERN = "{0}_text";

    [SerializeField] private Image _filledProgress;
    [SerializeField] private RectTransform _levelContainer;
    [SerializeField] private TextMeshProUGUI _rankLevels;

    private ThemeType _themeType;
    private PoolService _poolService;
    private UserRankType _rankLevelType;

    private float _offsetX;
    private bool _isNegativeDirection;
    private int _currentNumberElements;
    private Vector2 _lastObjectPosition;
    private int _numberElementsFitInLine;

    [Inject]
    public void Construct(PoolService poolService)
    {
        _poolService = poolService;
    }

    public void Setup(UserRankType userRankType, ThemeType themeType)
    {
        _themeType = themeType;
        _rankLevelType = userRankType;

        FillHeader();
        FillContent();
    }

    private void FillHeader()
    {
        _rankLevels.text = LocalizationProvider.GetText(LocalizationItemType.UI, string.Format(RANK_LOCALIZATION_PATTERN, _rankLevelType.ToString().ToLower()));
    }

    private void FillContent()
    {
        var levelsForRank = SettingsProvider.Get<ThemesSettings>().GetThemeSettings(_themeType).GetLevelByRank(_rankLevelType);

        foreach (var level in levelsForRank) 
        {
            var levelObject = _poolService.Get<LevelView>();
            levelObject.transform.SetParent(_levelContainer, false);
            
            levelObject.Setup(level.Id);
        }
    }
}
