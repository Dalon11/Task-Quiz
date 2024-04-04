using DG.Tweening;
using Quiz.Effects;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Quiz.Loading.Views
{
    public class LoadingView : MonoBehaviour
    {
        [SerializeField] private Image loadingScreenImage;
        [SerializeField] private float fadeDuration = 1f;

        private readonly GraphicEffects _effect = new GraphicEffects();

        public event Action OnCompleteStartLoading = () => { };
        public event Action OnCompleteStopLoading = () => { };

        private void Awake()
        {
            _effect.SetAlpha(loadingScreenImage, 0);
            loadingScreenImage.gameObject.SetActive(false);
        }

        public void ShowLoading()
        {
            loadingScreenImage.gameObject.SetActive(true);
            _effect.Fade(loadingScreenImage, 1, fadeDuration)
                .OnComplete(() => OnCompleteStartLoading.Invoke());
        }

        public void HideLoading()
        {
            _effect.Fade(loadingScreenImage, 0, fadeDuration)
               .OnComplete(() =>
               {
                   OnCompleteStopLoading.Invoke();
                   loadingScreenImage.gameObject.SetActive(false);
               });
        }
    }
}
