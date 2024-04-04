using Quiz.Cards.Models;
using Quiz.Cards.Models.Abstractions;
using Quiz.Cells;
using Quiz.Cells.Controllers;
using Quiz.Cells.Models.Abstractions;
using Quiz.Factory;
using Quiz.Levels.Models;
using Quiz.Levels.Views;
using UnityEngine;

namespace Quiz.Levels.Controllers
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private CardsContainer[] cardsContainer;
        [Space]
        [SerializeField] private LevelContainer levelContainer;
        [SerializeField] private TaskLevelView taskView;
        [SerializeField] private GridLevelView gridView;
        [SerializeField] private RestartLevelController restartController;
        [Space]
        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private Vector2 offset;

        [SerializeField] private float dureationPause = 2f;
        private int _currentLevel;

        private CellsFactory _factory;
        private readonly TaskHandler _taskHandler = new TaskHandler();

        private void Awake()
        {
            _factory = new CellsFactory(_taskHandler.ExcludedCards,
                cellPrefab.GetComponent<CellController>(), cellPrefab.GetComponent<SpriteRenderer>().bounds.size, offset);
        }

        private void Start()
        {
            _factory.onCreateGrid += CardsSelection;
            restartController.onRestartLevelStart += RestartLevel;
            restartController.onRestartLevelComplete += ShowCreateObjects;
            gridView.OnShowCreateComplete += OnShowCreateComplete;

            BeginLevel();
            ShowCreateObjects();
        }

        private void OnDestroy()
        {
            UnsubscribeCells();
            _factory.onCreateGrid -= CardsSelection;
            restartController.onRestartLevelStart -= RestartLevel;
            restartController.onRestartLevelComplete -= ShowCreateObjects;
            gridView.OnShowCreateComplete -= OnShowCreateComplete;
            _factory.Dispose();
        }

        public void BeginLevel()
        {
            CreateGrid();
            _factory.DeactivateGrid();
            taskView.ShowText(_taskHandler.CurrentCardTask.Identifier);
        }

        public void NextLevel()
        {
            CreateGrid();
            taskView.ShowText(_taskHandler.CurrentCardTask.Identifier);
            StartLevel();
        }

        private void StartLevel()
        {
            SubscribeCells();
            SetСlickable(true);
        }

        private void ShowCreateObjects()
        {
            Transform[] grid = new Transform[_factory.ActiveCells.Count];
            for (int i = 0; i < _factory.ActiveCells.Count; i++)
                grid[i] = _factory.ActiveCells[i].transform;

            gridView.ShowCreateObjects(grid);
        }
        private void OnShowCreateComplete()
        {
            taskView.SetActiveText(true);
            taskView.ShowTextFadeIn();
            StartLevel();
        }

        private void CardsSelection()
        {
            AbstractCardModel[] currentModel = cardsContainer[Random.Range(0, cardsContainer.Length)].CardModel;
            _taskHandler.SelectTask(currentModel);
            _factory.CreateCard(_taskHandler.CurrentCardTask, currentModel);
        }

        private void CreateGrid()
        {
            AbstractCellsModel currentCellModel = levelContainer.CellsModel[_currentLevel];
            _factory.CreateGrid(currentCellModel.NumberCellsHeight, currentCellModel.NumberCellsWidth);
        }

        private void CheckAnswer(AbstractCardModel item, IShowAnswer answerCell)
        {
            if (_taskHandler.CheckAnswer(item))
            {
                SetСlickable(false);
                answerCell.ShowCorrectAnswer();
                Invoke(nameof(EndLevel), dureationPause);
            }
            else
                answerCell.ShowIncorrectAnswer();
        }

        private void EndLevel()
        {
            UnsubscribeCells();
            _currentLevel++;
            if (_currentLevel >= levelContainer.CellsModel.Length)
                restartController.EndLevel();
            else
            {
                _factory.ClearGrid();
                NextLevel();
            }
        }

        private void RestartLevel()
        {
            _currentLevel = 0;
            _taskHandler.ResetTask();
            _factory.ClearGrid();
            taskView.SetActiveText(false);
            BeginLevel();
        }

        private void SubscribeCells()
        {
            for (int i = 0; i < _factory.ActiveCells.Count; i++)
                _factory.ActiveCells[i].OnAnswer += CheckAnswer;
        }

        private void UnsubscribeCells()
        {
            for (int i = 0; i < _factory.ActiveCells.Count; i++)
                _factory.ActiveCells[i].OnAnswer -= CheckAnswer;
        }

        private void SetСlickable(bool isСlickable)
        {
            for (int i = 0; i < _factory.ActiveCells.Count; i++)
                _factory.ActiveCells[i].IsСlickable = isСlickable;
        }
    }
}