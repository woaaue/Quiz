using System;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Quiz/LevelSettings", fileName = "LevelSettings", order = 1)]
public sealed class LevelSettings : ScriptableObject
{
    private const int COUNT_ANSWERS = 4;
    private const int COUNT_QUESTIONS = 5;

    [field: SerializeField] public string Id { get; private set; }
    [field: SerializeField] public int Number { get; private set; }
    [field: SerializeField] public UserRankType LevelRank { get; private set; }
    [field: SerializeField] public List<QuestionSettings> QuestionsSettings { get; private set; }

    public int GetMaxCountQuestions()
    {
        return COUNT_QUESTIONS;
    }

#if UNITY_EDITOR

    public void SetId()
    {
        Id = Guid.NewGuid().ToString();

        SaveAssets();
    }

    public void SetNumber(int numberLevel)
    {
        Number = numberLevel;

        SaveAssets();
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
            if (!QuestionsSettings.ElementAt(parameters.NumberQuestion).AnswersSettings.Any(answerSettings => answerSettings.IsCorrect) && !parameters.IsCorrect)
            {
                throw new Exception($"There are no correct answers to the question numbered: {parameters.NumberQuestion + 1}");
            }
        }

        if (parameters.IsCorrect && QuestionsSettings.ElementAt(parameters.NumberQuestion).AnswersSettings.Any(answerSettings => answerSettings.IsCorrect))
        {
            throw new Exception($"The question: {parameters.NumberQuestion + 1} already has the correct answer");
        }

        if (QuestionsSettings.ElementAt(parameters.NumberQuestion).AnswersSettings.Any(answerSettings => answerSettings.Id == parameters.Id))
        {
            throw new Exception($"In the question: {parameters.NumberQuestion + 1} there is already such an answer with id: {parameters.Id}");
        }

        QuestionsSettings.ElementAt(parameters.NumberQuestion).AnswersSettings.Add(new AnswerSettings
        {
            Id = parameters.Id,
            IsCorrect = parameters.IsCorrect,
        });

        SaveAssets();
    }

    public void SetQuestion(QuestionSettings question)
    {
        if (QuestionsSettings.Count == COUNT_QUESTIONS)
        {
            throw new Exception("Maximum number of questions exceeded");
        }

        if (QuestionsSettings.Any(questionSettings => questionSettings.Id == question.Id))
        {
            throw new Exception($"The question ({question.Id}) with the given id already exists at this level");
        }

        QuestionsSettings.Add(question);

        SaveAssets();
    }

    public void SaveAssets()
    {
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
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
