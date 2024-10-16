using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public sealed class StaggeredAnimation : MonoBehaviour
{
    private const float INTERVAL = 1.8f;
    private const float DISPLAY_TIME = 2f;
    private const float FADE_DURATION = 3f;
    private const int NUMBER_VISIBLE_IMAGES = 3;

    [SerializeField] private List<Image> _images;
    [SerializeField] private RectTransform _parentRect;

    private List<Image> _activeImages;
    private Queue<Image> _availableImages;

    private void Start()
    {
        _activeImages = new List<Image>();
        _availableImages = new Queue<Image>(_images);

        StartCoroutine(StaggeredAnimationRoutine());
    }

    private IEnumerator StaggeredAnimationRoutine()
    {
        while (true)
        {
            while (_activeImages.Count < NUMBER_VISIBLE_IMAGES) 
            {
                var nextImage = _availableImages.Dequeue();
                _activeImages.Add(nextImage);

                StartCoroutine(ShowAndHideRoutine(nextImage));

                yield return new WaitForSeconds(INTERVAL);
            }

            yield return new WaitUntil(() => _activeImages.Count < NUMBER_VISIBLE_IMAGES);
        }
    }

    private IEnumerator ShowAndHideRoutine(Image image)
    {
        image.transform.localPosition = GetRandomPosition();
        image.gameObject.SetActive(true);
        image.DOFade(1, FADE_DURATION);

        yield return new WaitForSeconds(DISPLAY_TIME);

        image.DOFade(0, FADE_DURATION).OnComplete(() =>
        {
            image.gameObject.SetActive(false);
            _activeImages.Remove(image);
            _availableImages.Enqueue(image);
        });
    }

    private Vector3 GetRandomPosition()
    {
        var randomX = Random.Range(-_parentRect.rect.width / 2, _parentRect.rect.width / 2);
        var randomY = Random.Range(-_parentRect.rect.height / 2, _parentRect.rect.height / 2);

        return new Vector3(randomX, randomY, 0);
    }
}
