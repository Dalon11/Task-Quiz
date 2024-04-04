using Quiz.Loading.Views;
using System;
using UnityEngine;

namespace Quiz.Loading.Controllers
{
    public class LoadingController : MonoBehaviour
    {
        [SerializeField] private LoadingView view;

        public event Action onLoadingStart = () => { };
        public event Action onLoadingComplete = () => { };

        private void Awake()
        {
            view.OnCompleteStartLoading += OnLoadingStart;
            view.OnCompleteStopLoading += OnLoadingComplete;
        }

        private void OnDestroy()
        {
            view.OnCompleteStartLoading -= OnLoadingStart;
            view.OnCompleteStopLoading -= OnLoadingComplete;
        }

        public void StartLoading() => view.ShowLoading();

        public void StopLoading() => view.HideLoading();

        private void OnLoadingStart() => onLoadingStart.Invoke();

        private void OnLoadingComplete() => onLoadingComplete.Invoke();


    }
}
