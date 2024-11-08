using TMPro;
using Zenject;
using UnityEngine;
using UnityEngine.UI;

public sealed class RankLevelsView : MonoBehaviour
{
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

        FillContent();
    }

    private void FillContent()
    {
        var levelsForRank = SettingsProvider.Get<ThemesSettings>().GetThemeSettings(_themeType).GetLevelByRank(_rankLevelType);

        foreach (var level in levelsForRank) 
        {
            var levelObject = _poolService.Get<LevelView>();

            SetOffsetAndNumberElements(levelObject.RectTransform);

            levelObject.transform.SetParent(_levelContainer, false);
            levelObject.Setup(level.Id, SetPosition());
        }
    }

    private void SetOffsetAndNumberElements(RectTransform rectTransform)
    {
        _numberElementsFitInLine = Mathf.FloorToInt(_levelContainer.rect.width / rectTransform.rect.width);
        _offsetX = _levelContainer.rect.width - (rectTransform.rect.width * _numberElementsFitInLine);
    }

    private Vector2 SetPosition()
    {
        if (_currentNumberElements == 0 && _lastObjectPosition == Vector2.zero)
        {
            _lastObjectPosition = new Vector2(-_levelContainer.rect.width / 2, _levelContainer.rect.height / 2);

            return _lastObjectPosition;
        }

        Vector2 newPosition;

        if (_currentNumberElements < _numberElementsFitInLine)
        {
            newPosition = _isNegativeDirection ? new Vector2(): new Vector2();
            _lastObjectPosition = newPosition;
            _currentNumberElements++;
        }
        else
        {
            _isNegativeDirection = !_isNegativeDirection;
            newPosition = new Vector2(_lastObjectPosition.x, _lastObjectPosition.y - _levelContainer.rect.height / 4);
            _lastObjectPosition = newPosition;
            _currentNumberElements = 1;
        }

        return newPosition;
    }
}
