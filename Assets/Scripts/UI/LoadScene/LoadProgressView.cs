using TMPro;
using UnityEngine;

public sealed class LoadProgressView : MonoBehaviour
{
    [SerializeField] private GameLoader _loader;
    [SerializeField] private TextMeshProUGUI _progress;

    private void Start()
    {
        _loader.ProgressChanged += OnProgressChanged;
    }

    private void OnDestroy()
    {
        _loader.ProgressChanged -= OnProgressChanged;
    }

    private void OnProgressChanged(float value)
    {
        _progress.text = value.ToString();
    }
}
