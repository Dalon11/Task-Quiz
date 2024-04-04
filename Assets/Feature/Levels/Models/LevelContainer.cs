using Quiz.Cells.Models;
using Quiz.Cells.Models.Abstractions;
using UnityEngine;

namespace Quiz.Levels.Models
{
    [CreateAssetMenu(fileName = "New" + nameof(LevelContainer), menuName = "Data/Level/" + nameof(LevelContainer))]
    public class LevelContainer : ScriptableObject
    {
        [SerializeField] private CellsModel[] cellsModel;

        public AbstractCellsModel[] CellsModel => cellsModel;
    }
}
