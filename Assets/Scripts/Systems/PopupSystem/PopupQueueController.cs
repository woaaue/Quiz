using UnityEngine;
using System.Collections.Generic;

public sealed class PopupQueueController : MonoBehaviour
{
    private const int POSITION_OFFSET_WITH_NAV_PANEL = 1;

    [SerializeField] private Transform _container;
    [SerializeField] private GameObject _background;

    private PopupSettings _settings;
    private PopupBase _currentPopup;
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

    public void HideAllPopups()
    {
        if (_queuePopups.Count > 0)
        {
            _currentPopup.Close();
            _queuePopups.Clear();
        }
    }

    private void ShowPopup()
    {
        if (!_background.activeInHierarchy)
        {
            _background.SetActive(true);
        }
        
        var popupPrefab = _queuePopups.Peek();

        if (popupPrefab.IsActiveNavigationPanel)
        {
            transform.SetSiblingIndex(POSITION_OFFSET_WITH_NAV_PANEL);
        }
        else
        {
            transform.SetAsLastSibling();
        }

        _currentPopup = Instantiate(popupPrefab, _container, false);

        _currentPopup.PopupClosed += HidePopup;
    }

    private void HidePopup()
    {
        if (_queuePopups.Count != 0)
        {
            _queuePopups.Dequeue();
        }

        _currentPopup.PopupClosed -= HidePopup;
        _currentPopup = null;

        if (_queuePopups.Count == 0)
        {
            _background.SetActive(false);
        }
        else
        {
            ShowPopup();
        }
    }

    private void Awake()
    {
        _queuePopups = new Queue<PopupBase>();
        _settings = SettingsProvider.Get<PopupSettings>();
    }
}
