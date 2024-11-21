using Zenject;
using UnityEngine;

public sealed class GamePopup : Popup<GamePopupSettings>
{
    [SerializeField] private GameManager _gameManager;
    
    private PopupService _popupService;

    [Inject]
    public void Construct(PopupService popupService)
    {
        _popupService = popupService;
    }

    public override void Setup(GamePopupSettings settings)
    {
        _gameManager.SetLevelData(settings.LevelSettings);

        _gameManager.GameEnded += OnGameEnded;

        base.Setup(settings);
    }

    private void OnDestroy()
    {
        _gameManager.GameEnded -= OnGameEnded;
    }

    private void OnGameEnded(string id, int countCorrectAnswers)
    {
        _popupService.ShowResultPopup(new ResultPopupSettings(id, countCorrectAnswers));
        Close();
    }
}

public sealed class GamePopupSettings : PopupBaseSettings
{
    public LevelSettings LevelSettings { get; private set; }

    public GamePopupSettings(LevelSettings levelSettings) 
    {
        LevelSettings = levelSettings;
    }
}
