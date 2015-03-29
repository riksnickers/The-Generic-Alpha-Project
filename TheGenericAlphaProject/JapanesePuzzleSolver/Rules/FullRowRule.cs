﻿using System;
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
        private bool _ran = false;
        public CellValue[][] ApplyRule(CellGrid cellGrid)
        {
            if (_ran)
            {
                return cellGrid.Cells;
            }
            var cells = cellGrid.Cells;

            //Iterate rows
            for (int row = 0; row < cellGrid.RowCount; row++)
            {
                //Get a row
                var rowHeader = cellGrid.RowHeaders.ElementAtOrDefault(row);
                if (rowHeader != null)
                {
                    if (rowHeader.HasSingleValue())
                    {
                        //Single Value
                        int rowHeaderValue = rowHeader.Values.First().Value;
                        if (rowHeaderValue == cellGrid.ColumnCount)
                        {
                            for (int col = 0; col < cellGrid.ColumnCount; col++)
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
                        if (totalFilledInCells + miniumGaps == cellGrid.ColumnCount)
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
            _ran = true;
            return cells;
        }
    }
}
