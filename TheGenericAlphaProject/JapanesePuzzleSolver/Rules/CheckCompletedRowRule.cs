using System;
using System.Data;
using System.Linq;

namespace TheJapanesePuzzleSolver.Rules
{
    public class CheckCompletedRowRule : IRule
    {
        public CellValue[][] ApplyRule(CellGrid cellGrid)
        {
            var cells = cellGrid.Cells;

            for (int row = 0; row < cellGrid.RowCount; row++)
            {
                bool rowCompleted = true;
                var rowHeader = cellGrid.RowHeaders.ElementAt(row);
                var rowBlocks = cells.GetRowFilledInBlockSizes(row);
                if (rowHeader.Values.Count == rowBlocks.Count)
                {
                    for (int index = 0; index < rowHeader.Values.Count; index++)
                    {
                        if (rowHeader.Values[index].Value != rowBlocks[index])
                        {
                            rowCompleted = false;
                        }
                    }
                }
                else
                {
                    rowCompleted = false;
                }

                if (rowCompleted)
                {
                    for (int col = 0; col < cells[row].Length; col++)
                    {
                        if (cells[row][col] == CellValue.Overlap || cells[row][col] == CellValue.Unknown)
                        {
                            cells[row][col] = CellValue.Empty;
                        }
                    }
                }
            }

            return cells;
        }
    }
}