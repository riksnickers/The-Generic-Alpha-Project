using System;
using System.Linq;
using TheJapanesePuzzleSolver.Exceptions;

namespace TheJapanesePuzzleSolver.Rules
{
    public class RowBorderRule : IRule
    {
        public CellValue[][] ApplyRule(CellGrid cellGrid)
        {
            var cells = cellGrid.Cells;

            for (int row = 0; row < cells.Length; row++)
            {
                var rowHeader = cellGrid.RowHeaders.ElementAt(row);
                CellValue firstRowCell = cells[row][0];
                if (firstRowCell == CellValue.FilledIn)
                {
                    var firstRowHeaderValue = rowHeader.Values.First();
                    int cell = 0;
                    for (; cell < firstRowHeaderValue.Value; cell++)
                    {
                        cells[row][cell] = CellValue.FilledIn;
                    }
                    if (cell <= cellGrid.ColumnCount)
                    {
                        cells[row][cell] = CellValue.Empty;
                    }
                }

                CellValue lastRowCell = cells[row][cellGrid.ColumnCount - 1];
                if (lastRowCell == CellValue.FilledIn)
                {
                    var lastRowHeaderValue = rowHeader.Values.Last();
                    int cell = cellGrid.RowCount - 1;
                    for (; cell > cellGrid.RowCount - lastRowHeaderValue.Value; cell--)
                    {
                        cells[row][cell] = CellValue.FilledIn;
                    }
                    cell--;
                    if (cell >= 0)
                    {
                        cells[row][cell] = CellValue.Empty;
                    }
                }
            }

            return cells;
        }
    }
}