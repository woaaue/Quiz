using TMPro;
using Zenject;
using UnityEngine;
using UnityEngine.UI;

public sealed class RankLevelsView : MonoBehaviour
{
    [SerializeField] private Image _filledProgress;
    [SerializeField] private Transform _levelContainer;
    [SerializeField] private TextMeshProUGUI _rankLevels;

    private UserInfo _userInfo;
    private ThemeType _themeType;
    private UserRankType _rankLevelType;

    [Inject]
    public void Construct(UserInfo userInfo)
    {
        _userInfo = userInfo;
    }

    public void Setup(UserRankType userRankType, ThemeType themeType)
    {
        _themeType = themeType;
        _rankLevelType = userRankType;
    }
}
