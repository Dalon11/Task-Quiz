using Quiz.Effects;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Quiz.Cells.Views
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer cellRenderer;
        [SerializeField] private SpriteRenderer iconRenderer;
        [SerializeField] private SpriteRenderer backgroundRenderer;
        [Space]
        [SerializeField] private float durationShake = 1f;
        [SerializeField] private Vector3 strengthShake = new Vector3(0.1f, 0f, 0f);
        [Space]
        [SerializeField] private float durationBounce = 1f;
        [SerializeField] private float scaleBounce = 1.5f;
        [Space]
        [SerializeField] private ParticleSystem particle;

        private ParticleCellView _particleView;
        private Quaternion _startRotationIcon;

        private readonly TransformEffects _effectView = new TransformEffects();
        private readonly string _regularExpressions = @"-rotate(-?\d+)$";

        private void Awake()
        {
            _particleView = new ParticleCellView(particle);
            _startRotationIcon = iconRenderer.transform.rotation;
        }

        public void SetIcon(Sprite icon)
        {
            iconRenderer.sprite = icon;
            RotateIcon(icon.name);
        }

        public void SetBackgroundColor(Color backgroundColor) => backgroundRenderer.color = backgroundColor;

        public void ShowIncorrectAnswer() => _effectView.DOShake(iconRenderer.transform, durationShake, strengthShake);

        public void ShowCorrectAnswer()
        {
            _particleView.PlayParticle();
            _effectView.Bounce(iconRenderer.transform, scaleBounce, durationBounce);
        }

        private void RotateIcon(string spriteName)
        {
            iconRenderer.transform.rotation = _startRotationIcon;
            Match match = Regex.Match(spriteName, _regularExpressions);
            if (!match.Success)
                return;

            iconRenderer.transform.Rotate(0f, 0f, -float.Parse(match.Groups[1].Value));
        }
    }
}
