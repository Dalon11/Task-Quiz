using Quiz.Cards.Models.Abstractions;
using Quiz.Cells.Views;
using Quiz.ClickHandlers;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Quiz.Cells.Controllers
{
    [SelectionBase]
    public class CellController : MonoBehaviour, IShowAnswer
    {
        [SerializeField] private CellView _view;

        private IClickHandler _clickHandler;
        private AbstractCardModel _currentCardData;

        public bool Is—lickable { get; set; }

        public event Action<AbstractCardModel, CellController> OnAnswer;

        private void Awake()
        {
            _clickHandler = GetComponent<IClickHandler>();
            _clickHandler.OnClick += Answer;
        }

        private void OnDestroy() => _clickHandler.OnClick -= Answer;

        public void SetCard(AbstractCardModel card)
        {
            _currentCardData = card;
            _view.SetBackgroundColor(Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));
            _view.SetIcon(card.Sprite);
        }

        public void ShowIncorrectAnswer() => _view.ShowIncorrectAnswer();

        public void ShowCorrectAnswer() => _view.ShowCorrectAnswer();

        private void Answer()
        {
            if (!Is—lickable)
                return;

            OnAnswer?.Invoke(_currentCardData, this);
        }
    }
}
