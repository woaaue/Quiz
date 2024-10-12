using System;
using System.IO;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;
using NaughtyAttributes;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Quiz/SettingsCreator", fileName = "SettingsCreator", order = 1)]
public sealed class SettingsCreator : ScriptableObject
{
    private const string PATH = "Localization";

    [SerializeField] private CreateSetting _createSetting;
    [SerializeField] private ThemesSettings _themesSettings;

#if UNITY_EDITOR

    [Button("CreateSettings")]
    private void CreateSettings()
    {
        _createSetting.GenerateId();

        Action typeCreateElement = _createSetting.Type switch
        {
            LocalizationItemType.Question => CreateQuestion,
            LocalizationItemType.Answer => CreateAnswer,
            LocalizationItemType.UI => CreateUI,
            _ => null
        };

        typeCreateElement?.Invoke();

        _createSetting = new CreateSetting();
    }

    private void CreateQuestion()
    {
        var dataItems = GetLocalizationFile();
        var themeSettings = _themesSettings.ThemeSettings.First(themeSetting => themeSetting.Type == _createSetting.Theme);
        var level = themeSettings.Levels.LevelsSetting.First(levelSettings => levelSettings.Number == _createSetting.Level);

        if (CheckMacth(dataItems, out var dataId))
        {
            level.SetQuestion(new QuestionSettings(dataId));
        }
        else
        {
            level.SetQuestion(new QuestionSettings(_createSetting.Id));
            AddItem(dataItems);
            SaveLocalizationFile(dataItems);
        }
    }

    private void CreateAnswer()
    {
        var dataItems = GetLocalizationFile();
        var themeSettings = _themesSettings.ThemeSettings.First(themeSetting => themeSetting.Type == _createSetting.Theme);
        var level = themeSettings.Levels.LevelsSetting.First(levelSettings => levelSettings.Number == _createSetting.Level);

        if (CheckMacth(dataItems, out var dataId))
        {
            level.SetAnswer(new AnswerParameters
            {
                Id = dataId,
                IsCorrect = _createSetting.IsCorrect,
                NumberQuestion = _createSetting.NumberForQuestion - 1
            });
        }
        else
        {
            level.SetAnswer(new AnswerParameters
            {
                Id = _createSetting.Id,
                IsCorrect = _createSetting.IsCorrect,
                NumberQuestion = _createSetting.NumberForQuestion - 1
            });

            AddItem(dataItems);
            SaveLocalizationFile(dataItems);
        }
    }

    private void CreateUI()
    {
        var dataItems = GetLocalizationFile();

        if (!CheckMacth(dataItems, out var dataId))
        {
            AddItem(dataItems);
            SaveLocalizationFile(dataItems);
        }
    }

    private void AddItem(Dictionary<LanguageType, List<LocalizationItem>> dataItems)
    {
        foreach (var localizationItem in _createSetting.LanguageSettings)
        {
            dataItems[localizationItem.LanguageType].Add(new LocalizationItem
            {
                Key = _createSetting.Id,
                Text = localizationItem.Text,
            });
        }
    }

    private void SaveLocalizationFile(Dictionary<LanguageType, List<LocalizationItem>> dataItems)
    {
        foreach (KeyValuePair<LanguageType, List<LocalizationItem>> kvp in dataItems)
        {
            var jsonData = JsonConvert.SerializeObject(kvp.Value);
            var path = Path.Combine(Application.streamingAssetsPath, PATH, kvp.Key.ToString(), _createSetting.Type.ToString());

            File.WriteAllText(path, jsonData);
        }
    }

    private bool CheckMacth(Dictionary<LanguageType, List<LocalizationItem>> dataItems, out string dataId)
    {
        var languagesSettings = _createSetting.LanguageSettings;

        foreach (var languageSettings in languagesSettings)
        {
            foreach (var item in dataItems[languageSettings.LanguageType])
            {
                if (item.Text == languageSettings.Text)
                {
                    if (_createSetting.Type != LocalizationItemType.UI)
                    {
                        dataId = item.Key;
                        return true;
                    }

                    var path = Path.Combine(Application.streamingAssetsPath, PATH, languageSettings.LanguageType.ToString(), _createSetting.Type.ToString());

                    throw new Exception($"This text is already located at path: {path} and its id: {item.Key}");
                }    
            }
        }

        dataId = null;
        return false;
    }

    private Dictionary<LanguageType, List<LocalizationItem>> GetLocalizationFile()
    {
        var localizationFile = new Dictionary<LanguageType, List<LocalizationItem>>(); 
        var path = Path.Combine(Application.streamingAssetsPath, PATH);

        foreach(LanguageType type in Enum.GetValues(typeof(LanguageType)))
        {
            var languagePath = Path.Combine(path, type.ToString());

            var localizationItems = new List<LocalizationItem>();

            foreach (LocalizationItemType itemType in Enum.GetValues(typeof(LocalizationItemType)))
            {
                if (itemType == _createSetting.Type)
                {
                    var filePath = Path.Combine(languagePath, $"{itemType}.json");
                    var jsonData = File.ReadAllText(filePath);
                    localizationItems = JsonConvert.DeserializeObject<List<LocalizationItem>>(jsonData);
                }
            }

            localizationFile[type] = localizationItems;
        }

        return localizationFile;
    }

#endif

}

[Serializable]
public sealed class CreateSetting
{
    public string Id { get; private set; }

    public LocalizationItemType Type;
    public ThemeType Theme;
    public int Level;
    public bool IsCorrect;
    public int NumberForQuestion;
    public List<LanguageSetting> LanguageSettings;

    public void GenerateId()
    {
        Id = Guid.NewGuid().ToString();
    }
}

[Serializable]
public sealed class AnswerParameters
{
    public string Id;
    public bool IsCorrect;
    public int NumberQuestion;
}

[Serializable]
public sealed class LanguageSetting
{
    public LanguageType LanguageType;
    public string Text;
}
