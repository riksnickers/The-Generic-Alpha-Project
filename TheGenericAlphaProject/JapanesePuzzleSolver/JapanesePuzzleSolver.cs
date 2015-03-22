using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace TheJapanesePuzzleSolver
{
    public class JapanesePuzzleSolver
    {
        public Grid Grid { get; set; }

        public bool[][] Analyse()
        {
            var cells = new bool[Grid.Rows][];
            for (int row = 0; row < Grid.Rows; row++)
            {
                cells[row] = Grid.AnalyzeRow(row);
            }
            return cells;

        }
    }

    public class Grid
    {
        public Grid(IEnumerable<IEnumerable<int>> columns, IEnumerable<IEnumerable<int>> rows)
        {
            RowValues = new List<Row>();
            ColumnValues = new List<Column>();

            foreach (IEnumerable<int> column in columns)
            {
                ColumnValues.Add(new Column(column));
            }

            foreach (IEnumerable<int> row in rows)
            {
                RowValues.Add(new Row(row));
            }

            Cells = new CellValue[Rows][];
            for (int row = 0; row < Rows; row++)
            {
                Cells[row] = new CellValue[Columns];
                for (int col = 0; col < Columns; col++)
                {
                    Cells[row][col] = new CellValue();
                }
            }
            
        }

        public List<Row> RowValues { get; private set; }
        public List<Column> ColumnValues { get; private set; }
        public CellValue[][] Cells { get; private set; }

        public int Rows
        {
            get { return RowValues.Count; }
        }

        public int Columns
        {
            get { return ColumnValues.Count; }
        }

        public bool[] AnalyzeRow(int index)
        {
            var returnRow = new List<bool>();
            var row = RowValues.ElementAt(index);
            if (row.Values.Count == 1 && row.Values.First().Value == Columns)
            {
                for (int i = 0; i < Columns; i++)
                {
                    returnRow.Add(true);
                }
            }
            return returnRow.ToArray();
        }
    }

    public class Row
    {
        public Row(IEnumerable<int> rowValues)
        {
            Values = new List<RowValue>();
            foreach (var rowValue in rowValues)
            {
                Values.Add(new RowValue(Values.Count, rowValue));
            }
        }

        public List<RowValue> Values { get; private set; }
    }

    public class RowValue
    {
        public RowValue(int order, int value)
        {
            Order = order;
            Value = value;
        }

        public int Order { get; private set; }
        public int Value { get; private set; }
    }

    public class Column
    {
        public Column(IEnumerable<int> columnValues)
        {
            Values = new List<ColumnValue>();
            foreach (var columnValue in columnValues)
            {
                Values.Add(new ColumnValue(Values.Count, columnValue));
            }
        }

        public List<ColumnValue> Values { get; private set; }
    }

    public class ColumnValue
    {
        public ColumnValue(int order, int value)
        {
            Order = order;
            Value = value;
        }

        public int Order { get; private set; }
        public int Value { get; private set; }
    }

    public enum CellValue
    {
        Unknown,
        Overlap,
        FilledIn,
        Empty,
    }
}
