using System.Collections.Generic;

namespace TheJapanesePuzzleSolver
{
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
}