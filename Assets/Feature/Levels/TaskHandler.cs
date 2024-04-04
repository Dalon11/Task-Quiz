using Quiz.Cards.Models.Abstractions;
using System.Collections.Generic;
using UnityEngine;

namespace Quiz.Levels
{
    public class TaskHandler
    {
        private AbstractCardModel _currentTaskCard;
        private readonly List<AbstractCardModel> _excludedCards = new List<AbstractCardModel>();

        public AbstractCardModel CurrentCardTask => _currentTaskCard;
        public IReadOnlyList<AbstractCardModel> ExcludedCards => _excludedCards;

        public void SelectTask(AbstractCardModel[] allModel)
        {
            _currentTaskCard = allModel[Random.Range(0, allModel.Length)];
            _excludedCards.Add(_currentTaskCard);
        }

        public bool CheckAnswer(AbstractCardModel answerModel) => _currentTaskCard?.Identifier == answerModel.Identifier;

        public void ResetTask() => _excludedCards.Clear();
    }
}
