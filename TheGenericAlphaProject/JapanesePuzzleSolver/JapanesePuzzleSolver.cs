using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapanesePuzzleSolver
{
    public class JapanesePuzzleSolver
    {
        
    }

    public class Grid
    {
        private SortedList<int, Row> _rowValues;
        private SortedList<int, Column> _columnValues;

        public SortedList<int, Row> RowValues
        {
            get
            {
                if (_rowValues == null)
                {
                    _rowValues = new SortedList<int, Row>();
                }
                return _rowValues;
            }
        }

        public SortedList<int, Column> ColumnValues
        {
            get
            {
                if (_columnValues == null)
                {
                    _columnValues = new SortedList<int, Column>();
                }
                return _columnValues;
            }
        }

        public int Rows
        {
            get { return RowValues.Count; }
        }

        public int Columns
        {
            get { return ColumnValues.Count; }
        }

        public void AddRow(Row row)
        {
            RowValues.Add(RowValues.Count + 0, row);
        }

        public void AddColumn(Column column)
        {
            ColumnValues.Add(RowValues.Count + 0, column);
        }
    }

    public class Row
    {
        public SortedList<int, int> Values { get; set; }
    }

    public class Column
    {
        public SortedList<int, int> Values { get; set; } 
    }
}
