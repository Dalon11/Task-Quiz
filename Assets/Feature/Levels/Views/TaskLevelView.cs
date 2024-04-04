using Quiz.Effects;
using UnityEngine;
using UnityEngine.UI;

namespace Quiz.Levels.Views
{
    public class TaskLevelView : MonoBehaviour
    {
        [SerializeField] private Text taskText;
        [SerializeField] private string formatTaskText = "Find {0}";
        [SerializeField] private float fadeDuration = 1f;

        private readonly GraphicEffects _effect = new GraphicEffects();

        public void ShowText(string text) => taskText.text = string.Format(formatTaskText, text);

        public void SetActiveText(bool isActive) => taskText.gameObject.SetActive(isActive);

        public void ShowTextFadeIn()
        {
            _effect.SetAlpha(taskText, 0);
            _effect.Fade(taskText, 1, fadeDuration);
        }
    }
}
