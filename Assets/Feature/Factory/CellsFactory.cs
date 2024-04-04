using Quiz.Cards.Models.Abstractions;
using Quiz.Cells.Controllers;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Quiz.Factory
{
    using Creators;

    public class CellsFactory : IDisposable
    {
        private readonly CellController _cellPrefab;
        private readonly GridCreator<CellController> _gridCreator;
        private readonly CardCreator _cardCreator;
        private readonly Vector2 _cellSize;
        private readonly Vector2 _offset;

        public IReadOnlyList<CellController> ActiveCells { get; private set; }

        public event Action onCreateGrid = () => { };

        public CellsFactory(IReadOnlyList<AbstractCardModel> excludeTask, CellController cellPrefab, Vector2 cellSize, Vector2 offset)
        {
            _cellPrefab = cellPrefab;
            _offset = offset;
            _cellSize = cellSize;
            _gridCreator = new GridCreator<CellController>(_cellPrefab);
            _cardCreator = new CardCreator(excludeTask);
        }

        public void Dispose() => _gridCreator.Dispose();

        public void DeactivateGrid() => _gridCreator.DeactivateGrid();

        public void ClearGrid() => _gridCreator.ClearGrid();


        public void CreateGrid(int numberCellsHeight, int numberCellsWidth)
        {
            ActiveCells = _gridCreator.CreateGrid((numberCellsWidth, numberCellsHeight), _cellSize, _offset);
            _cardCreator.SetActiveCells(ActiveCells);
            onCreateGrid.Invoke();
        }

        public void CreateCard(AbstractCardModel task, in AbstractCardModel[] data)
        {
            _cardCreator.CreateCards(task, data);
        }
    }
}
