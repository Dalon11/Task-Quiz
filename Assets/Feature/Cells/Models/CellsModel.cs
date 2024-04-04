using System;
using UnityEngine;

namespace Quiz.Cells.Models
{
    using Abstractions;

    [Serializable]
    public class CellsModel : AbstractCellsModel
    {
        [SerializeField, Min(0)] private int numberCellsHeight;
        [SerializeField, Min(0)] private int numberCellsWidth;

        public override int NumberCellsHeight => numberCellsHeight;
        public override int NumberCellsWidth => numberCellsWidth;
    }
}
