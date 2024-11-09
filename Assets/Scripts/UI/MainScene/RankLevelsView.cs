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
            var rectOffset = new Vector2(levelObject.RectTransform.rect.width, levelObject.RectTransform.rect.height);

            SetOffsetAndNumberElements(levelObject.RectTransform);

            levelObject.transform.SetParent(_levelContainer, false);
            
            levelObject.Setup(level.Id, SetPosition(rectOffset));
        }
    }

    private void SetOffsetAndNumberElements(RectTransform rectTransform)
    {
        _numberElementsFitInLine = Mathf.FloorToInt(_levelContainer.rect.width / rectTransform.rect.width);
        _offsetX = (_levelContainer.rect.width - (rectTransform.rect.width * _numberElementsFitInLine)) / (_numberElementsFitInLine + 1);
    }

    private Vector2 SetPosition(Vector2 rectOffset)
    {
        if (_currentNumberElements == 0 && _lastObjectPosition == Vector2.zero)
        {
            _lastObjectPosition = new Vector2(-_levelContainer.rect.width / 2 + _offsetX + rectOffset.x / 2, _levelContainer.rect.height / 2 - rectOffset.y / 2);
            _currentNumberElements++;

            return _lastObjectPosition;
        }

        Vector2 newPosition;

        if (_currentNumberElements < _numberElementsFitInLine)
        {
            SetTransform();
            _currentNumberElements++;
        }
        else
        {
            _isNegativeDirection = !_isNegativeDirection;

            SetTransform();
            _currentNumberElements = 1;
        }

        return newPosition;

        void SetTransform()
        {
            newPosition = _isNegativeDirection
                ? new Vector2(_lastObjectPosition.x - _offsetX - rectOffset.x, _lastObjectPosition.y - rectOffset.y)
                : new Vector2(_lastObjectPosition.x + _offsetX + rectOffset.x, _lastObjectPosition.y - rectOffset.y);

            _lastObjectPosition = newPosition;
        }
    }
}
