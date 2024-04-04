using Quiz.Levels.Views;
using Quiz.Loading.Controllers;
using System;
using UnityEngine;

namespace Quiz.Levels.Controllers
{
    public class RestartLevelController : MonoBehaviour
    {
        [SerializeField] private LoadingController loadingController;
        [SerializeField] private RestartLevelView restartView;

        public event Action onRestartLevelStart = () => { };
        public event Action onRestartLevelComplete = () => { };

        private void Start()
        {
            loadingController.onLoadingStart += RestartLevel;
            loadingController.onLoadingComplete += LoadingComplete;
            restartView.onRestartButton += StartLoad;
        }

        private void OnDestroy()
        {
            loadingController.onLoadingStart -= RestartLevel;
            loadingController.onLoadingComplete -= LoadingComplete;
            restartView.onRestartButton -= StartLoad;
        }

        public void EndLevel() => restartView.ShowRestartPanel();

        private void StartLoad()
        {
            loadingController.StartLoading();
            Invoke(nameof(StopLoad), 2f);
        }

        private void RestartLevel() => onRestartLevelStart.Invoke();

        private void LoadingComplete() => onRestartLevelComplete.Invoke();

        private void StopLoad() => loadingController.StopLoading();
    }
}