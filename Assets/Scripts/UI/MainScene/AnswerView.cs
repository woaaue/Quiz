using TMPro;
using System;
using UnityEngine;
using JetBrains.Annotations;

public sealed class AnswerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _answer;

    public event Action AnimationFinished;
    public event Action<bool> AnswerSelected;

    private AnswerSettings _answerSettings;

    [UsedImplicitly]
    public void Select()
    {
        AnswerSelected?.Invoke(_answerSettings.IsCorrect);
    }

    public void SetAnswerData(AnswerSettings settings)
    {
        _answerSettings = settings;
        _answer.text = LocalizationProvider.GetText(LocalizationItemType.Answer, _answerSettings.Id);
    }
}
