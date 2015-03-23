using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using TheJapanesePuzzleSolver.Exceptions;

namespace TheJapanesePuzzleSolver.Rules
{
    public class FullRowRule : IRule
    {
        public CellValue[][] ApplyRule(Grid grid)
        {
            var cells = grid.Cells;

            //Iterate rows
            for (int row = 0; row < grid.RowCount; row++)
            {
                //Get a row
                var rowHeader = grid.RowHeaders.ElementAtOrDefault(row);
                if (rowHeader != null)
                {
                    if (rowHeader.HasSingleValue())
                    {
                        //Single Value
                        int rowHeaderValue = rowHeader.Values.First().Value;
                        if (rowHeaderValue == grid.ColumnCount)
                        {
                            for (int col = 0; col < grid.ColumnCount; col++)
                            {
                                cells[row][col] = CellValue.FilledIn;
                            }
                        }
                    }
                    else
                    {
                        //Sum of the headers + the single gaps
                        int totalFilledInCells = rowHeader.Values.Sum(rowValue => rowValue.Value);
                        int miniumGaps = rowHeader.Values.Count - 1;
                        if (totalFilledInCells + miniumGaps == grid.ColumnCount)
                        {
                            int col = 0;
                            foreach (RowValue rowValue in rowHeader.Values)
                            {
                                for (int blockCounter = 0; blockCounter < rowValue.Value; blockCounter++)
                                {
                                    cells[row][col] = CellValue.FilledIn;
                                    col++;
                                }
                                if (rowHeader.Values.IndexOf(rowValue) < rowHeader.Values.Count - 1)
                                {
                                    cells[row][col] = CellValue.Empty;
                                    col++;
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

            return cells;
        }
    }
}
