using System;
using System.IO;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks;
using System.Collections.Generic;

public static class LocalizationProvider
{
    public static event Action LanguageChanged;

    private static LanguageType _currentLanguage;
    private static Dictionary<LanguageType, LocalizationGroup> _localizationData;

    public static void SetLanguage(LanguageType language)
    {
        _currentLanguage = language;
    }

    public static void SwitchLanguage(LanguageType language)
    {
        if (_currentLanguage != language)
        {
            _currentLanguage = language;

            LanguageChanged?.Invoke();
        }
    }

    public static async Task InitializeAsync(string path)
    {
       await LoadLocalizationAsync(path);
    }

    public static string GetText(LocalizationItemType itemType, string key) => GetLocalizedTextByKey(_currentLanguage, itemType, key);
    public static string GetText(LocalizationItemType itemType, params string[] tags) => GetLocalizedTextByTags(_currentLanguage, itemType, tags);

    private static string GetLocalizedTextByKey(LanguageType language, LocalizationItemType itemType, string key)
    {
        var localizationGroup = _localizationData[language];

        return localizationGroup[itemType].FirstOrDefault(item => item.Key == key).Text;
    }

    private static string GetLocalizedTextByTags(LanguageType language, LocalizationItemType itemType, params string[] tags)
    {
        var localizationGroup = _localizationData[language];

        return localizationGroup[itemType].FirstOrDefault(item => item.Tags != null
        && tags.All(tag => item.Tags.Contains(tag))).Text;
    }

    private static async Task LoadLocalizationAsync(string path)
    {
        _localizationData = new Dictionary<LanguageType, LocalizationGroup>();
        var localizationPath = Path.Combine(Application.streamingAssetsPath, path);

        foreach (LanguageType language in Enum.GetValues(typeof(LanguageType)))
        {
            var languagePath = Path.Combine(localizationPath, language.ToString());

            if (!Directory.Exists(languagePath))
            {
                continue;
            }

            var itemDictionary = new LocalizationGroup();

            foreach (LocalizationItemType itemType in Enum.GetValues(typeof(LocalizationItemType)))
            {
                var itemPath = Path.Combine(languagePath, itemType.ToString());

                if (!Directory.Exists(itemPath))
                {
                    continue;
                }

                var items = await LoadItemsAsync(itemPath);
                itemDictionary.Add(itemType, items);
            }

            _localizationData.Add(language, itemDictionary);
        }
    }

    private static async Task<List<LocalizationItem>> LoadItemsAsync(string folderPath)
    {
        var items = new List<LocalizationItem>();

        string[] jsonFiles = Directory.GetFiles(folderPath, "*.json");

        foreach (var jsonFile in jsonFiles)
        {
            var jsonContent = await File.ReadAllTextAsync(jsonFile);
            LocalizationItem item = JsonUtility.FromJson<LocalizationItem>(jsonContent);

            items.Add(item);
        }

        return items;
    }
}