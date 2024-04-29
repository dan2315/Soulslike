using DG.Tweening;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] RectTransform _background;
    [SerializeField] RectTransform _fillElement;
    [SerializeField] RectTransform _fillElementLingering;

    private Tween _activeTween;


    public void Change(float fillAmount) // value between 0 and 1
    {
        fillAmount = 1 - fillAmount;
        Vector2 targetValue = new Vector2(-_background.rect.width * fillAmount, _fillElement.offsetMax.y);
        _fillElement.offsetMax = targetValue;

        _activeTween?.Kill();
        _activeTween = DOVirtual.Vector3(_fillElementLingering.offsetMax, targetValue, 1f, value => {
            _fillElementLingering.offsetMax = value;
        }).SetEase(Ease.Linear);
    }
}