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
        EventSystem.Subscribe<ProgressLoadSignal>(OnProgressChanged);   
    }

    private void OnDestroy()
    {
        EventSystem.Unsubscribe<ProgressLoadSignal>(OnProgressChanged);
    }

    private void OnProgressChanged(ProgressLoadSignal signal)
    {
        _progressBar.fillAmount = signal.Progress / 1f;
        _value.text = string.Format(TEMPLATE, signal.Progress * 100);
    }
}
