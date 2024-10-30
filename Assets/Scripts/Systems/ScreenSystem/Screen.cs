using System;
using DG.Tweening;
using UnityEngine;

public abstract class Screen : MonoBehaviour, IWindow
{
    private const float SHOW_DURATION = 0.5f;

    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private RectTransform _buttonRectTransform;

    private Vector2 _targetPosition = Vector2.zero;

    public void Show(Action callback)
    {
        gameObject.SetActive(true);
        ShowAnimation(callback);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowAnimation(Action callback)
    {
        float screenWidth = UnityEngine.Screen.width;
        float screenHeight = UnityEngine.Screen.height;

        var screenCenter = new Vector2(screenWidth / 2, screenHeight / 2);
        var buttonPosition = _buttonRectTransform.position;

        if (buttonPosition.x > screenCenter.x)
        {
            _rectTransform.anchoredPosition = new Vector2(screenWidth, 0);
        }
        else
        {
            _rectTransform.anchoredPosition = new Vector2(-screenHeight, 0);
        }

        _rectTransform.DOAnchorPos(_targetPosition, SHOW_DURATION)
            .SetEase(Ease.OutBack)
            .SetLink(gameObject)
            .OnComplete(() =>
            {
                callback.Invoke();
            });
    }
}
