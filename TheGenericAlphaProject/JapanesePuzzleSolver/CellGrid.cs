using System.Collections.Generic;
using System.Linq;

namespace TheJapanesePuzzleSolver
{
    public class CellGrid
    {
        public CellGrid(IEnumerable<IEnumerable<int>> columns, IEnumerable<IEnumerable<int>> rows)
        {
            RowHeaders = new List<Row>();
            ColumnHeaders = new List<Column>();

            foreach (IEnumerable<int> column in columns)
            {
                ColumnHeaders.Add(new Column(column));
            }

            foreach (IEnumerable<int> row in rows)
            {
                RowHeaders.Add(new Row(row));
            }

            Cells = new CellValue[RowCount][];
            for (int row = 0; row < RowCount; row++)
            {
                Cells[row] = new CellValue[ColumnCount];
                for (int col = 0; col < ColumnCount; col++)
                {
                    Cells[row][col] = new CellValue();
                }
            }

        }

        public List<Row> RowHeaders { get; private set; }
        public List<Column> ColumnHeaders { get; private set; }

        public CellValue[][] Cells { get; set; }

        public int RowCount
        {
            get { return RowHeaders.Count; }
        }

        public int ColumnCount
        {
            get { return ColumnHeaders.Count; }
        }

        public bool[] AnalyzeRow(int index)
        {
            var returnRow = new List<bool>();
            var row = RowHeaders.ElementAt(index);
            if (row.Values.Count == 1 && row.Values.First().Value == ColumnCount)
            {
                for (int i = 0; i < ColumnCount; i++)
                {
                    returnRow.Add(true);
                }
            }
            return returnRow.ToArray();
        }
    }

    public static class GridExtensions
    {
        public static bool IsSolved(this CellValue[][] cells)
        {
            if (cells.Any(row => row.Any(col => col == CellValue.Overlap || col == CellValue.Unknown)))
            {
                return false;
            }
            return true;
        }

        public static List<int> GetRowFilledInBlockSizes(this CellValue[][] cells, int row)
        {
            List<int> blocks = new List<int>();

            int tempBlock = 0;
            for (int col = 0; col < cells[row].Length; col++)
            {
                CellValue value = cells[row][col];
                if (value == CellValue.FilledIn)
                {
                    tempBlock++;
                }
                else if ((value == CellValue.Overlap || value == CellValue.Unknown || value == CellValue.Empty) && tempBlock > 0)
                {
                    blocks.Add(tempBlock);
                    tempBlock = 0;
                }
            } 
            if (tempBlock > 0)
            {
                blocks.Add(tempBlock);
            }

            return blocks;
        }

        public static List<int> GetColumnFilledInBlockSizes(this CellValue[][] cells, int column)
        {
            List<int> blocks = new List<int>();

            int tempBlock = 0;
            for (int row = 0; row < cells.Length; row++)
            {
                CellValue value = cells[row][column];
                if (value == CellValue.FilledIn)
                {
                    tempBlock++;
                }
                else if ((value == CellValue.Overlap || value == CellValue.Unknown || value == CellValue.Empty) && tempBlock > 0)
                {
                    blocks.Add(tempBlock);
                    tempBlock = 0;
                }
            }
            if (tempBlock > 0)
            {
                blocks.Add(tempBlock);
            }
            return blocks;
        }
    }
}