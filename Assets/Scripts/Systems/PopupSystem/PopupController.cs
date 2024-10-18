using UnityEngine;

public sealed class PopupController : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private GameObject _background;

    private PopupBase _currentPopup;
    private PopupSettings _settings;

    public void ShowPopup<T>(T settings) where T : PopupBaseSettings
    {
        if (_currentPopup != null) 
        {
            _currentPopup.ClosePopup();
        }

        var popupPrefab = _settings.Get<T>();
        var instance = Instantiate(popupPrefab, _container, false);

        instance.Setup(settings);

        _currentPopup = instance;
        _background.SetActive(true);

        EventSystem.Subscribe<HidePopupSignal>(HidePopup);
    }

    private void Start()
    {
        _settings = SettingsProvider.Get<PopupSettings>();
    }

    private void HidePopup(HidePopupSignal signal)
    {
        _currentPopup = null;

        if (!signal.IsActive)
        {
            _background.SetActive(false);
        }
    }
}
