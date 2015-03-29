using System.Linq;

namespace TheJapanesePuzzleSolver.Rules
{
    public class CheckCompletedColumnRule : IRule
    {
        public CellValue[][] ApplyRule(CellGrid cellGrid)
        {
            var cells = cellGrid.Cells;

            for (int col = 0; col < cellGrid.ColumnCount; col++)
            {
                bool columnCompleted = true;
                var columnHeader = cellGrid.ColumnHeaders.ElementAt(col);
                var columnBlocks = cells.GetColumnFilledInBlockSizes(col);
                if (columnHeader.Values.Count == columnBlocks.Count)
                {
                    for (int index = 0; index < columnHeader.Values.Count; index++)
                    {
                        if (columnHeader.Values[index].Value != columnBlocks[index])
                        {
                            columnCompleted = false;
                        }
                    }
                }
                else
                {
                    columnCompleted = false;
                }

                if (columnCompleted)
                {
                    foreach (CellValue[] cell in cells)
                    {
                        if (cell[col] == CellValue.Overlap || cell[col] == CellValue.Unknown)
                        {
                            cell[col] = CellValue.Empty;
                        }
                    }
                }
            }

            return cells;
        }
    }
}