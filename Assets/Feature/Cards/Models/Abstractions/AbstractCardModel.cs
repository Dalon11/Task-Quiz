using UnityEngine;

namespace Quiz.Cards.Models.Abstractions
{
    public abstract class AbstractCardModel
    {
        public abstract Sprite Sprite { get; }
        public abstract string Identifier { get; }
    }
}
