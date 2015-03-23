using System.Collections.Generic;
using System.Linq;
using TheJapanesePuzzleSolver.Exceptions;

namespace TheJapanesePuzzleSolver
{
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

        public bool HasSingleValue()
        {
            if (!Values.Any())
            {
                throw new InvalidRowHeaderException();
            }
            return Values.Count == 1;
        }
    }
}