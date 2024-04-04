using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Quiz.Effects
{
    public class GraphicEffects
    {
        private Tween _tween;

        public void SetAlpha(Graphic graphic, float alpha)
        {
            Color newColor = graphic.color;
            newColor.a = alpha;
            graphic.color = newColor;
        }

        public Tween Fade(Graphic graphic, float to, float duration)
        {
            _tween?.Kill();
            return _tween = graphic.DOFade(to, duration);
        }
    }
}
