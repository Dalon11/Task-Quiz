using Quiz.Effects;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Quiz.Levels.Views
{
    public class RestartLevelView : MonoBehaviour
    {
        [SerializeField] private Image restartPanel;
        [SerializeField] private Button restartButton;
        [SerializeField] private float fadeDuration = 1f;

        private readonly GraphicEffects _effect = new GraphicEffects();

        public event Action onRestartButton = () => { };

        private void Awake()
        {
            restartButton.onClick.AddListener(() =>
            {
                onRestartButton.Invoke();
                HideRestartPanel();
            });
            HideRestartPanel();
        }

        private void OnDestroy() => restartButton.onClick.RemoveAllListeners();

        public void ShowRestartPanel()
        {
            restartPanel.gameObject.SetActive(true);
            _effect.Fade(restartPanel, 0.7f, fadeDuration);
        }

        public void HideRestartPanel()
        {
            restartPanel.gameObject.SetActive(false);
            _effect.SetAlpha(restartPanel, 0);
        }
    }
}
