using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Quiz/LevelSettings", fileName = "LevelSettings", order = 1)]
public sealed class LevelSettings : ScriptableObject
{
    private const int COUNT_QUESTIONS = 5;

    [field: SerializeField] public string Id { get; private set; }
    [field: SerializeField] public int Number { get; private set; }
    [field: SerializeField] public List<QuestionSettings> QuestionSettings { get; private set; }

#if UNITY_EDITOR

    public void SetId()
    {
        Id = Guid.NewGuid().ToString();

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public void SetQuestionSettings(ThemeType type)
    {
        QuestionSettings = new List<QuestionSettings>();

        for (var i = 0; i < COUNT_QUESTIONS; i++)
        {
            var counter = 0;

            if (Number == 1)
                counter = 1;
            else
                counter = --Number * COUNT_QUESTIONS;

            QuestionSettings.Add(
                    new QuestionSettings
                    {
                        Type = type,
                        Number = counter + i
                    });
        }

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

#endif
}

[Serializable]
public sealed class QuestionSettings
{
    public int Number;
    public ThemeType Type;
}
