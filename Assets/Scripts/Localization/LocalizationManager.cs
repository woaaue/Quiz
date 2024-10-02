using System;
using System.IO;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public sealed class LocalizationManager
{
    private const string PATH = "Localization";
    private Dictionary<LanguageType, LocalizationGroup> _localizationData;

    public LocalizationManager()
    {
        LoadLocalization();
    }

    public string GetLocalizedTextByKey(LanguageType language, LocalizationItemType itemType, string key)
    {
        var localizationGroup = _localizationData[language];
        
        return localizationGroup[itemType].FirstOrDefault(item => item.Key == key).Text;
    }

    public string GetLocalizedTextByTags(LanguageType language, LocalizationItemType itemType, params string[] tags)
    {
        var localizationGroup = _localizationData[language];

        return localizationGroup[itemType].FirstOrDefault(item => item.Tags != null 
        && tags.All(tag => item.Tags.Contains(tag))).Text;
    }

    private void LoadLocalization()
    {
        _localizationData = new Dictionary<LanguageType, LocalizationGroup>();
        var localizationPath = Path.Combine(Application.streamingAssetsPath, PATH);

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

                var items = LoadItems(itemPath);
                itemDictionary.Add(itemType, items);
            }

            _localizationData.Add(language, itemDictionary);
        }
    }

    private List<LocalizationItem> LoadItems(string folderPath)
    {
        var items = new List<LocalizationItem>();

        string[] jsonFiles = Directory.GetFiles(folderPath, "*.json");

        foreach (var jsonFile in jsonFiles)
        {
            var jsonContent = File.ReadAllText(jsonFile);
            LocalizationItem item = JsonUtility.FromJson<LocalizationItem>(jsonContent);

            items.Add(item);
        }

        return items;
    }
}
