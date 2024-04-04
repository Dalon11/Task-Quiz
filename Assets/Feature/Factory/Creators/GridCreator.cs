using System;
using System.Collections.Generic;
using UnityEngine;

namespace Quiz.Factory.Creators
{
    public class GridCreator<T> : IDisposable where T : MonoBehaviour
    {
        private readonly ObjectPool<T> _cellsPool;
        private readonly List<T> _grid = new List<T>();

        public GridCreator(T prefab) => _cellsPool = new ObjectPool<T>(prefab);

        public IReadOnlyList<T> CreateGrid((int x, int y) numberCells, Vector2 cellSize, Vector2 offset)
        {
            int totalCells = numberCells.x * numberCells.y;
            Vector2 startSpawnPosition = new Vector2(GetSpawnPoint(numberCells.x, cellSize.x), -GetSpawnPoint(numberCells.y, cellSize.y));
            startSpawnPosition += offset;
            Vector2 currentPosition = startSpawnPosition;
            for (int i = 0; i < totalCells; i++)
            {
                if (i > 0 && i % numberCells.x == 0)
                {
                    currentPosition.x = startSpawnPosition.x;
                    currentPosition.y -= cellSize.y;
                }

                SetCellPosition(currentPosition);
                currentPosition.x += cellSize.x;
            }

            return _grid;
        }

        public void DeactivateGrid()
        {
            for (int i = 0; i < _grid.Count; i++)
                _grid[i].gameObject.SetActive(false);
        }

        public void ClearGrid()
        {
            DeactivateGrid();
            _grid.Clear();
        }

        private void SetCellPosition(Vector2 position)
        {
            T cell = _cellsPool.Get();
            cell.transform.position = position;
            cell.gameObject.SetActive(true);
            _grid.Add(cell);
        }

        private float GetSpawnPoint(int number, float size) => (size - number * size) / 2f;

        public void Dispose() => _cellsPool.Dispose();
    }
}
