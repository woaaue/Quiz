using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using JetBrains.Annotations;

public sealed class AnswerView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _answer;

    public event Action<bool> Selected;

    private AnswerSettings _answerSettings;

    [UsedImplicitly]
    public void Select()
    {
        if (_answerSettings.IsCorrect)
        {
            _image.color = Color.green;
        }
        else
        {
            _image.color = Color.red;
        }

        Selected?.Invoke(_answerSettings.IsCorrect);
    }

    public void SetAnswerData(AnswerSettings settings)
    {
        _answerSettings = settings;
        _image.color = Color.white;
        _answer.text = LocalizationProvider.GetText(LocalizationItemType.Answer, _answerSettings.Id);
    }
}
