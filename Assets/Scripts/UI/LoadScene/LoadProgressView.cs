using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class LoadProgressView : MonoBehaviour
{
    private const string TEMPLATE = "{0} %";

    [SerializeField] private Image _progressBar;
    [SerializeField] private TextMeshProUGUI _value;

    private void Start()
    {
        EventSystem.Subscribe<ProgressLoadEvent>(OnProgressChanged);   
    }

    private void OnDestroy()
    {
        EventSystem.Unsubscribe<ProgressLoadEvent>(OnProgressChanged);
    }

    private void OnProgressChanged(ProgressLoadEvent loadEvent)
    {
        _progressBar.fillAmount = loadEvent.Progress / 1f;
        _value.text = string.Format(TEMPLATE, loadEvent.Progress * 100);
    }
}
