using System;
using UnityEngine;
using DG.Tweening;
using JetBrains.Annotations;

public class PopupBase : MonoBehaviour
{
    private const float ANIMATION_DURATION = 0.5f;

    public event Action PopupClosed;

    [SerializeField] private CanvasGroup _canvasGroup;
    [field: SerializeField] public bool IsActiveNavigationPanel { get; private set; }

    [UsedImplicitly]
    public virtual void Close()
    {
        Hide(Destroy);
    }

    private void Awake()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }

    private protected void Start()
    {
        Show();
    }

    private protected void Destroy()
    {
        PopupClosed?.Invoke();
        Destroy(gameObject);
    }

    private void Show()
    {
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;

        _canvasGroup.DOFade(1, ANIMATION_DURATION)
            .SetLink(gameObject);
    }

    private void Hide(Action callback)
    {
        _canvasGroup.DOFade(0, ANIMATION_DURATION)
             .SetLink(gameObject)
             .OnComplete(() =>
             {
                 _canvasGroup.interactable = false;
                 _canvasGroup.blocksRaycasts = false;

                 callback.Invoke();
             });
    }
}
