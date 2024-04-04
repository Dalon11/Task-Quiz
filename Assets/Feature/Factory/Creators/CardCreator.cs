using Quiz.Cards.Models.Abstractions;
using Quiz.Cells.Controllers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Quiz.Factory.Creators
{
    public class CardCreator
    {
        private IReadOnlyList<CellController> _activeCardCells;
        private readonly IReadOnlyList<AbstractCardModel> _excludedCards;

        public CardCreator(IReadOnlyList<AbstractCardModel> excludedCards) => _excludedCards = excludedCards;

        public void SetActiveCells(IReadOnlyList<CellController> activeCells) => _activeCardCells = activeCells;

        public void CreateCards(AbstractCardModel taskModel, AbstractCardModel[] allModel)
        {
            int taskIndex = Random.Range(0, _activeCardCells.Count);
            _activeCardCells[taskIndex].SetCard(taskModel);

            CreateRandomCards(allModel, taskIndex);
        }

        private void CreateRandomCards(AbstractCardModel[] allCards, int skipIndex)
        {
            var cardsList = new List<AbstractCardModel>(allCards);
            for (int i = 0; i < _activeCardCells.Count; i++)
            {
                if (i == skipIndex)
                    continue;

                _activeCardCells[i].SetCard(GetRandomCard(cardsList, allCards));
            }
        }

        private AbstractCardModel GetRandomCard(List<AbstractCardModel> cardsList, AbstractCardModel[] allCards)
        {
            int count = 0;
            int index;
            AbstractCardModel card;
            do
            {
                if (count++ > allCards.Length * _excludedCards.Count)
                    throw new System.Exception("Tasks over.");

                if (cardsList.Count == 0)
                    cardsList.AddRange(allCards);

                index = Random.Range(0, cardsList.Count);
                card = cardsList[index];
                cardsList.RemoveAt(index);
            }
            while (_excludedCards.Contains(card));

            return card;
        }
    }
}
