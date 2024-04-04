using System;
using UnityEngine;

namespace Quiz.ClickHandlers
{
    public class ClickComponent : MonoBehaviour, IClickHandler
    {
        public event Action OnClick = () => { };

        private void OnMouseDown() => OnClick.Invoke();
    }
}
