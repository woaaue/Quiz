using UnityEngine;
using System.Collections.Generic;

public sealed class PopupQueueController : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private GameObject _background;

    private PopupSettings _settings;
    private Queue<PopupBase> _queuePopups;

    public void AddPopup<T>(T settings) where T : PopupBaseSettings
    {
        var popupPrefab = _settings.Get<T>();

        popupPrefab.Setup(settings);
        _queuePopups.Enqueue(popupPrefab);

        if (_queuePopups.Count == 1)
        {
            ShowPopup();
        }
    }

    private void ShowPopup()
    {
        if (!_background.activeInHierarchy)
        {
            _background.SetActive(true);
        }

        var instance = Instantiate(_queuePopups.Peek(), _container, false);

        EventSystem.Subscribe<HidePopupEvent>(HidePopup);
    }

    private void HidePopup(HidePopupEvent popupEvent)
    {
        _queuePopups.Dequeue();

        if (_queuePopups.Count == 0)
        {
            _background.SetActive(false);
        }
    }

    private void Awake()
    {
        _queuePopups = new Queue<PopupBase>();
        _settings = SettingsProvider.Get<PopupSettings>();
    }
}
