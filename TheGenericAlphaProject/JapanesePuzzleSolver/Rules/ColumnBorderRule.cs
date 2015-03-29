using System.Linq;

namespace TheJapanesePuzzleSolver.Rules
{
    public class ColumnBorderRule : IRule
    {
        public CellValue[][] ApplyRule(CellGrid cellGrid)
        {
            var cells = cellGrid.Cells;

            for (int col = 0; col < cells.Length; col++)
            {
                var colHeader = cellGrid.ColumnHeaders.ElementAt(col);
                CellValue firstColumnCell = cells[0][col];
                if (firstColumnCell == CellValue.FilledIn)
                {
                    var firstColumnHeaderValue = colHeader.Values.First();
                    int cell = 0;
                    for (; cell < firstColumnHeaderValue.Value; cell++)
                    {
                        cells[cell][col] = CellValue.FilledIn;
                    }
                    if (cell <= cellGrid.ColumnCount)
                    {
                        cells[cell][col] = CellValue.Empty;
                    }
                }

                CellValue lastColumnCell = cells[cellGrid.RowCount - 1][col];
                if (lastColumnCell == CellValue.FilledIn)
                {
                    var lastColumnHeaderValue = colHeader.Values.Last();
                    int cell = cellGrid.ColumnCount - 1;
                    for (; cell > cellGrid.ColumnCount - lastColumnHeaderValue.Value; cell--)
                    {
                        cells[cell][col] = CellValue.FilledIn;
                    }
                    cell--;
                    if (cell >= 0)
                    {
                        cells[cell][col] = CellValue.Empty;
                    }
                }
            }

            return cells;
        }
    }
}