using TMPro;
using Zenject;
using UnityEngine;
using UnityEngine.UI;
using JetBrains.Annotations;

public sealed class LevelView : MonoBehaviour
{
    [SerializeField] private Image _filledProgress;
    [SerializeField] private TextMeshProUGUI _numberLevel;

    private string _id;
    private UserInfo _userInfo;

    [Inject]
    public void Construct(UserInfo userInfo)
    {
        _userInfo = userInfo;
    }

    public void Setup(string id)
    {
        _id = id;
    }

    [UsedImplicitly]
    public void OpenLevel()
    {

    }
}
