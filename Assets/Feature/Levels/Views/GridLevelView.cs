using DG.Tweening;
using Quiz.Cells.Controllers;
using Quiz.Effects;
using System;
using UnityEngine;

namespace Quiz.Levels.Views
{
    public class GridLevelView : MonoBehaviour
    {
        [SerializeField] private float durationBounce = 1f;
        [SerializeField] private float scaleBounce = 1.5f;

        private Sequence _sequence;
        private readonly TransformEffects _effectView = new TransformEffects();

        public event Action OnShowCreateComplete = () => { };

        public void ShowCreateObjects(Transform[] cellsGrid)
        {
            _sequence.Kill();
            _sequence = DOTween.Sequence();
            foreach (Transform cell in cellsGrid)
                _sequence.Append(_effectView.Bounce(cell, scaleBounce, durationBounce))
                    .OnComplete(() => OnShowCreateComplete.Invoke());

            //_sequence.OnComplete(() => OnShowCreateComplete.Invoke());

        }
    }
}
