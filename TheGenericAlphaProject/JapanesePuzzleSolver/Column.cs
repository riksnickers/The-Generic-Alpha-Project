using System.Collections.Generic;
using System.Linq;
using TheJapanesePuzzleSolver.Exceptions;

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

        public bool HasSingleValue()
        {
            if (!Values.Any())
            {
                throw new InvalidColumnHeaderException();
            }
            return Values.Count == 1;
        }
    }
}