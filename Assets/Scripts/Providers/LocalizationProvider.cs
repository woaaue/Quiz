using System;
using System.IO;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

public static class LocalizationProvider
{
    private const string EXTENSION = ".json";

    public static event Action LanguageChanged;
    
    public static LanguageType CurrentLanguage { get; private set; }

    private static Dictionary<LanguageType, LocalizationGroup> _localizationData;

    public static void SetLanguage(LanguageType language)
    {
        CurrentLanguage = language;
    }

    public static void SwitchLanguage(LanguageType language)
    {
        if (CurrentLanguage != language)
        {
            CurrentLanguage = language;

            LanguageChanged?.Invoke();
        }
    }

    public static async Task InitializeAsync(string path)
    {
        await LoadLocalizationAsync(path);
    }

    public static string GetText(LocalizationItemType itemType, string key) => GetLocalizedTextByKey(CurrentLanguage, itemType, key);

    private static string GetLocalizedTextByKey(LanguageType language, LocalizationItemType itemType, string key)
    {
        var localizationGroup = _localizationData[language];

        return localizationGroup[itemType].FirstOrDefault(item => item.Key == key).Text;
    }

    private static async Task LoadLocalizationAsync(string path)
    {
        _localizationData = new Dictionary<LanguageType, LocalizationGroup>();
        var localizationPath = Path.Combine(Application.streamingAssetsPath, path);
        var languageTypes = Enum.GetValues(typeof(LanguageType)).Cast<LanguageType>();
        var itemTypes = Enum.GetValues(typeof(LocalizationItemType)).Cast<LocalizationItemType>();

        foreach (var language in languageTypes)
        {
            var languagePath = Path.Combine(localizationPath, language.ToString());

            if (!Directory.Exists(languagePath))
            {
                continue;
            }

            var itemDictionary = new LocalizationGroup();
            
            foreach (var itemType in itemTypes)
            {
                var itemPath = Path.Combine(languagePath, $"{itemType}{EXTENSION}");

                if (!File.Exists(itemPath))
                {
                    continue;
                }

                var items = await LoadItemsAsync(itemPath);
                itemDictionary.Add(itemType, items);
            }

            _localizationData.Add(language, itemDictionary);
        }
    }

    private static async Task<List<LocalizationItem>> LoadItemsAsync(string itemPath)
    {
        var jsonContent = await File.ReadAllTextAsync(itemPath);
        var localizationItems = JsonConvert.DeserializeObject<List<LocalizationItem>>(jsonContent);

        return localizationItems;
    }
}