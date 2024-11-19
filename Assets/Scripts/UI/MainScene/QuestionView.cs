using TMPro;
using UnityEngine;

public sealed class QuestionView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _question;

    public void SetQuestion(string id)
    {
        _question.text = LocalizationProvider.GetText(LocalizationItemType.Question, id);
    }
}
