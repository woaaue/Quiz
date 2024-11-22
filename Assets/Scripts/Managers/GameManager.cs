using TMPro;
using System;
using UnityEngine;
using System.Collections;
using Random = System.Random;
using System.Collections.Generic;

public sealed class GameManager : MonoBehaviour
{
    private const string PATTERN_PROGRESS = "{0} {1}/{2}";
    private const string LOCALIZATION_KEY = "question_text";

    [SerializeField] private TextMeshProUGUI _progress;
    [SerializeField] private QuestionView _questionView;
    [SerializeField] private List<AnswerView> _answersView;
    [SerializeField] private GameObject _raycastTargetObject;

    public event Action<string, int> GameEnded;

    private int _currentQuestion;
    private int _maxQuestionCount;
    private int _countCorrectAnswers;
    private string _progressLocalization;
    private LevelSettings _levelSettings;

    public void SetLevelData(LevelSettings settings)
    {
        _levelSettings = settings;
        _maxQuestionCount = _levelSettings.GetMaxCountQuestions();
        _progressLocalization = LocalizationProvider.GetText(LocalizationItemType.UI, LOCALIZATION_KEY);

        FilledQuestionSettings();
        _raycastTargetObject.SetActive(false);
    }

    private void Start()
    {
        foreach (var answer in _answersView)
        {
            answer.Selected += OnSelectedAnswer;
        }
    }

    private void OnDestroy()
    {
        foreach (var answer in _answersView)
        {
            answer.Selected -= OnSelectedAnswer;
        }
    }

    private void FilledQuestionSettings()
    {
        if (_currentQuestion < _maxQuestionCount)
        {
            var counter = 0;

            ShuffleListButtons();
            _questionView.SetQuestion(_levelSettings.QuestionsSettings[_currentQuestion].Id);

            foreach (var answer in _answersView)
            {
                answer.SetAnswerData(_levelSettings.QuestionsSettings[_currentQuestion].AnswersSettings[counter]);
                counter++;
            }

            _currentQuestion += 1;
            _progress.text = string.Format(PATTERN_PROGRESS, _progressLocalization, _currentQuestion, _maxQuestionCount);
        }
    }

    private void OnSelectedAnswer(bool isCorrect)
    {
        if (isCorrect)
        {
            _countCorrectAnswers += 1;
        }

        StartCoroutine(LatencyRoutine());
    }

    private IEnumerator LatencyRoutine()
    {
        _raycastTargetObject.SetActive(true);

        yield return new WaitForSecondsRealtime(1f);

        if (_currentQuestion < _maxQuestionCount)
        {
            FilledQuestionSettings();
        }
        else
        {
            GameEnded?.Invoke(_levelSettings.Id, _countCorrectAnswers);

            yield break;
        }

        _raycastTargetObject.SetActive(false);

        yield break;
    }

    private void ShuffleListButtons()
    {
        var random = new Random();
        var count = _answersView.Count;

        for (int i = count - 1; i > 0; i--)
        {
            int randomIndex;

            do
            {
                randomIndex = random.Next(0, count);
            } 
            while (randomIndex == i);

            (_answersView[i], _answersView[randomIndex]) = (_answersView[randomIndex], _answersView[i]);
        }
    }
}
