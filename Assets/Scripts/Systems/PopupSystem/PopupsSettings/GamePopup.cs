using UnityEngine;

public sealed class GamePopup : Popup<GamePopupSettings>
{
    [SerializeField] private GameManager _gameManager;
    
    public override void Setup(GamePopupSettings settings)
    {
        _gameManager.SetLevelData(settings.LevelSettings);
        base.Setup(settings);
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
