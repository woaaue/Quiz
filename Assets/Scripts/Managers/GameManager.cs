using TMPro;
using UnityEngine;
using System.Collections.Generic;

public sealed class GameManager : MonoBehaviour
{
    private const string PATTERN_PROGRESS = "{0} {1}/{2}";
    private const string LOCALIZATION_KEY = "question_text";

    [SerializeField] private TextMeshProUGUI _progress;
    [SerializeField] private QuestionView _questionView;
    [SerializeField] private List<AnswerView> _answersView;
    [SerializeField] private GameObject _raycastTargetObject;

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

    //private void Start()
    //{
    //    foreach (var answer in _answersView)
    //    {
            
    //    }
    //}

    //private void OnDestroy()
    //{
    //    foreach (var answer in _answersView)
    //    {

    //    }
    //}

    private void FilledQuestionSettings()
    {
        if (_currentQuestion < _maxQuestionCount)
        {
            var counter = 0;
            _currentQuestion += 1;
            _questionView.SetQuestion(_levelSettings.QuestionsSettings[_currentQuestion].Id);

            foreach (var answer in _answersView)
            {
                answer.SetAnswerData(_levelSettings.QuestionsSettings[_currentQuestion].AnswersSettings[counter]);
                counter++;
            }

            _progress.text = string.Format(PATTERN_PROGRESS, _progressLocalization, _currentQuestion, _maxQuestionCount);
        }
    }

    private void OnSelectedAnswer(bool isCorrect)
    {
        if (isCorrect)
        {
            _countCorrectAnswers += 1;
        }

        _raycastTargetObject.SetActive(true);
    }
}
