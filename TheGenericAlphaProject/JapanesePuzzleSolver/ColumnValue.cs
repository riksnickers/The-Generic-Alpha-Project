namespace TheJapanesePuzzleSolver
{
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
}