using TMPro;
using UnityEngine;

public sealed class LocalizationText : MonoBehaviour
{
    [SerializeField] private string _key;
    [SerializeField] private LocalizationItemType _itemType;
    [SerializeField] private TextMeshProUGUI _container;

    private void Start()
    {
        OnLanguageChanged();

       LocalizationProvider.LanguageChanged += OnLanguageChanged;
    }

    private void OnDestroy()
    {
        LocalizationProvider.LanguageChanged -= OnLanguageChanged;
    }

    private void OnLanguageChanged()
    {
        _container.text = LocalizationProvider.GetText(_itemType, _key);
    }
}
