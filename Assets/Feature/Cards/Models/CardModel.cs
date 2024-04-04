using System;
using UnityEngine;

namespace Quiz.Cards.Models
{
    using Abstractions;

    [Serializable]
    public class CardModel : AbstractCardModel
    {
        [SerializeField] private Sprite sprite;
        [SerializeField] private string identifier;

        public override Sprite Sprite => sprite;
        public override string Identifier => identifier;

        public CardModel(Sprite sprite, string identifier)
        {
            this.sprite = sprite;
            this.identifier = identifier;
        }
    }
}
