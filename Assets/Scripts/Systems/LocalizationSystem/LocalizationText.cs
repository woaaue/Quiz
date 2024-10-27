using TMPro;
using UnityEngine;

public sealed class LocalizationText : MonoBehaviour
{
    [SerializeField] private string _key;
    [SerializeField] private LocalizationItemType _itemType;
    [SerializeField] private TextMeshProUGUI _container;

    private void Start()
    {
        _container.text = LocalizationProvider.GetText(_itemType, _key);

        EventSystem.Subscribe<ChangeLanguageEvent>(OnLanguageChanged);
    }

    private void OnDestroy()
    {
        EventSystem.Subscribe<ChangeLanguageEvent>(OnLanguageChanged);
    }

    private void OnLanguageChanged(ChangeLanguageEvent languageEvent)
    {
        _container.text = LocalizationProvider.GetText(_itemType, _key);
    }
}
