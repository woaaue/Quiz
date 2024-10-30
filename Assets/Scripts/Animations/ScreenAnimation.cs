using UnityEngine;
using DG.Tweening;
using JetBrains.Annotations;

public sealed class ScreenAnimation : MonoBehaviour
{
    private const float DURATION = 0.5f;

    [SerializeField] private RectTransform _screenToAnimate;

    private Vector2 _targetPosition = Vector2.zero;

    [UsedImplicitly]
    public void ShowScreenFromButton(RectTransform buttonRect)
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Vector2 buttonPosition = buttonRect.position;

        if (buttonPosition.x > screenCenter.x)
        {
            _screenToAnimate.anchoredPosition = new Vector2(Screen.width, 0);
        }
        else
        {
            _screenToAnimate.anchoredPosition = new Vector2(-Screen.width, 0);
        }

        _screenToAnimate.DOAnchorPos(_targetPosition, DURATION)
            .SetEase(Ease.OutBack);
    }
}

