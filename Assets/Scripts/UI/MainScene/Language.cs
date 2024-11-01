using TMPro;
using UnityEngine;
using JetBrains.Annotations;

public sealed class Language : MonoBehaviour
{
    private const string KEY = "{0}_popup_text";

    [SerializeField] private GameObject _selectImage;
    [SerializeField] private TextMeshProUGUI _localizationText;

    private LanguageType _languageType;

    public void Setup(LanguageType language)
    {
        _languageType = language;

        OnChangeLanguage();
    }

    [UsedImplicitly]
    public void SelectLanguage()
    {
        if (LocalizationProvider.CurrentLanguage != _languageType)
        {
            LocalizationProvider.SwitchLanguage(_languageType);
        }
    }

    private void Start()
    {
        LocalizationProvider.LanguageChanged += OnChangeLanguage;
    }

    private void OnDestroy()
    {
        LocalizationProvider.LanguageChanged -= OnChangeLanguage;
    }

    private void OnChangeLanguage()
    {
        _localizationText.text = LocalizationProvider.GetText(LocalizationItemType.UI, string.Format(KEY, _languageType.ToString().ToLower()));
        _selectImage.SetActive(LocalizationProvider.CurrentLanguage == _languageType);
    }
}
