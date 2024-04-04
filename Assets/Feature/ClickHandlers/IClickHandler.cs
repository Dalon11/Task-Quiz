using System;

namespace Quiz.ClickHandlers
{
    public interface IClickHandler
    {
        public event Action OnClick;
    }
}
