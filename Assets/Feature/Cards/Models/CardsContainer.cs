using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Quiz.Cards.Models
{
    using Abstractions;

    [CreateAssetMenu(fileName = "New" + nameof(CardsContainer), menuName = "Data/Cards/" + nameof(CardsContainer))]
    public class CardsContainer : ScriptableObject
    {
        [SerializeField] private CardModel[] cardModel;

        public AbstractCardModel[] CardModel => cardModel;

#if UNITY_EDITOR
        [Header("OnValidate")]
        [SerializeField] private bool useValidate = true;
        [SerializeField] private string regularExpressions = @"-id([^-\s]*)(?:[-]|$)";
        [SerializeField] private Sprite[] cardsSprites = new Sprite[0];

        private void OnValidate()
        {
            if (!useValidate)
                return;

            var models = new List<CardModel>();
            Regex regex = new Regex(regularExpressions);
            Match match;
            CardModel newModel;
            string identifier;
            foreach (Sprite sprite in cardsSprites)
            {
                match = regex.Match(sprite.name);
                identifier = match.Success ? match.Groups[1].Value : default;
                models.Add(newModel = new CardModel(sprite, identifier));
            }
            cardModel = models.ToArray();
        }
#endif
    }
}