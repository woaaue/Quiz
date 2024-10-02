using System;

public static class LocalizationProvider
{
    public static event Action LanguageChanged;

    private static LocalizationManager _loader;
    private static LanguageType _currentLanguage;

    static LocalizationProvider()
    {
        _loader = new LocalizationManager();
    }

    public static string GetText(LocalizationItemType itemType, string key) => _loader.GetLocalizedTextByKey(_currentLanguage, itemType, key);
    public static string GetText(LocalizationItemType itemType, params string[] tags) => _loader.GetLocalizedTextByTags(_currentLanguage, itemType, tags);

    public static void SwitchLanguage(LanguageType language)
    {
        if (_currentLanguage != language) 
        {
            _currentLanguage = language;

            LanguageChanged?.Invoke();
        }
    }
}