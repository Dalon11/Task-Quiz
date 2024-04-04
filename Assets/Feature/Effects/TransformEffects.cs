using DG.Tweening;
using UnityEngine;

namespace Quiz.Effects
{
    public class TransformEffects
    {
        private Sequence _sequence;
        private Tween _shakeTween;
        private Tween _bounceTween;

        public Sequence Bounce(Transform transform, float scale, float duretion)
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
            Vector3 startScale = transform.localScale;
            _sequence.Append(DOScale(transform, startScale * scale, duretion / 3, Ease.OutBounce))
                .Append(DOScale(transform, startScale * 0.7f, duretion / 3, Ease.InBounce))
                .Append(DOScale(transform, startScale, duretion / 3, Ease.OutBounce));
            return _sequence;
        }

        public Tween DOScale(Transform transform, Vector3 scale, float duretion, Ease easing)
        {
            _bounceTween?.Kill();
            Vector3 startScale = transform.localScale;
            _bounceTween = transform.DOScale(scale, duretion)
                .SetEase(easing)
                .OnPlay(() => transform.gameObject.SetActive(true))
                .OnKill(() => transform.localScale = startScale);
            return _bounceTween;
        }

        public Tween DOShake(Transform transform, float duration, Vector3 strength, Ease easing = Ease.InBounce)
        {
            _shakeTween?.Kill();
            Vector3 startPosition = transform.localPosition;
            _shakeTween = transform.DOShakePosition(duration, strength)
                .SetEase(easing)
                .OnKill(() => transform.localPosition = startPosition);
            return _shakeTween;
        }
    }
}
