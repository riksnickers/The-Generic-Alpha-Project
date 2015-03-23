namespace TheJapanesePuzzleSolver
{
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
}