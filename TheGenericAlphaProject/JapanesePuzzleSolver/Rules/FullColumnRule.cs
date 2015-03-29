using System.Linq;
using TheJapanesePuzzleSolver.Exceptions;

namespace TheJapanesePuzzleSolver.Rules
{
    public class FullColumnRule : IRule
    {
        private bool _ran = false;

        public CellValue[][] ApplyRule(CellGrid cellGrid)
        {
            if (_ran)
            {
                return cellGrid.Cells;
            }

            var cells = cellGrid.Cells;

            //Iterate rows
            for (int col = 0; col < cellGrid.RowCount; col++)
            {
                //Get a row
                var colHeader = cellGrid.ColumnHeaders.ElementAtOrDefault(col);
                if (colHeader != null)
                {
                    if (colHeader.HasSingleValue())
                    {
                        //Single Value
                        int rowHeaderValue = colHeader.Values.First().Value;
                        if (rowHeaderValue == cellGrid.ColumnCount)
                        {
                            for (int row = 0; row < cellGrid.RowCount; row++)
                            {
                                cells[row][col] = CellValue.FilledIn;
                            }
                        }
                    }
                    else
                    {
                        //Sum of the headers + the single gaps
                        int totalFilledInCells = colHeader.Values.Sum(columnValue => columnValue.Value);
                        int miniumGaps = colHeader.Values.Count - 1;
                        if (totalFilledInCells + miniumGaps == cellGrid.RowCount)
                        {
                            int row = 0;
                            foreach (ColumnValue columnValue in colHeader.Values)
                            {
                                for (int blockCounter = 0; blockCounter < columnValue.Value; blockCounter++)
                                {
                                    cells[row][col] = CellValue.FilledIn;
                                    row++;
                                }
                                if (colHeader.Values.IndexOf(columnValue) < colHeader.Values.Count - 1)
                                {
                                    cells[row][col] = CellValue.Empty;
                                    row++;
                                }
                            }
                        }
                    }
                }
                else
                {
                    throw new InvalidRowHeaderException();
                }

            }
            _ran = true;
            return cells;
        }
    }
}