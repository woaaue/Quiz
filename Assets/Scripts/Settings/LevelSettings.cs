using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(menuName = "Quiz/LevelSettings", fileName = "LevelSettings", order = 1)]
public sealed class LevelSettings : ScriptableObject
{
    private const int COUNT_ANSWERS = 4;
    private const int COUNT_QUESTIONS = 5;

    [field: SerializeField] public string Id { get; private set; }
    [field: SerializeField] public int Number { get; private set; }
    [field: SerializeField] public List<QuestionSettings> QuestionsSettings { get; private set; }

#if UNITY_EDITOR

    public void SetLevelId()
    {
        Id = Guid.NewGuid().ToString();

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public void SetAnswer(AnswerParameters parameters)
    {
        if (QuestionsSettings.Count < parameters.NumberQuestion)
        {
            throw new Exception($"Question number: {parameters.NumberQuestion + 1} does not exist");
        }

        if (QuestionsSettings.ElementAt(parameters.NumberQuestion).AnswersSettings.Count == COUNT_ANSWERS)
        {
            throw new Exception($"The maximum possible number of answers for a question {parameters.NumberQuestion + 1} has been exceeded");
        }

        if (QuestionsSettings.ElementAt(parameters.NumberQuestion).AnswersSettings.Count == 3)
        {
            if (!QuestionsSettings.ElementAt(parameters.NumberQuestion).AnswersSettings.Any(answerSettings => answerSettings.IsCorrect))
            {
                throw new Exception($"There are no correct answers to the question numbered: {parameters.NumberQuestion + 1}");
            }
        }

        QuestionsSettings.ElementAt(parameters.NumberQuestion).AnswersSettings.Add(new AnswerSettings
        {
            Id = parameters.Id,
            IsCorrect = parameters.IsCorrect,
        });

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public void SetQuestion(QuestionSettings question)
    {
        if (QuestionsSettings.Count == COUNT_QUESTIONS)
        {
            throw new Exception("Maximum number of questions exceeded");
        }

        CheckQuestion(question);

        QuestionsSettings.Add(question);

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private void CheckQuestion(QuestionSettings question)
    {
        foreach (var questionSetting in QuestionsSettings)
        {
            if (questionSetting.Id == question.Id)
            {
                throw new Exception($"The question ({question.Id}) with the given id already exists");
            }
        }
    }

#endif
}

[Serializable]
public sealed class QuestionSettings
{
    public string Id;
    public List<AnswerSettings> AnswersSettings;

    public QuestionSettings(string id)
    {
        Id = id;
        AnswersSettings = new List<AnswerSettings>();
    }
}

[Serializable]
public sealed class AnswerSettings
{
    public string Id;
    public bool IsCorrect;
}
