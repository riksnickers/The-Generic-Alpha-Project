using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using TheJapanesePuzzleSolver;

namespace JapanesePuzzleSolverWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Rectangle[][] Rectangles;
        private readonly Dictionary<CellValue, Brush> _brushes = new Dictionary<CellValue, Brush>
            {
                {CellValue.Empty, Brushes.LightGray},
                {CellValue.FilledIn, Brushes.ForestGreen},
                {CellValue.Overlap, Brushes.LawnGreen},
                {CellValue.Unknown, Brushes.Wheat},
            };

        private JapanesePuzzleSolver _jps;

        public MainWindow()
        {
            InitializeComponent();

            _jps = new JapanesePuzzleSolver();

            List<List<int>> colList = new List<List<int>>
            {
                new List<int>{3,6},
                new List<int>{3,4},
                new List<int>{3,4},
                new List<int>{1},
                new List<int>{1,1},
                new List<int>{1,1},
                new List<int>{1,1,1,3},
                new List<int>{2,5},
                new List<int>{2,3},
                new List<int>{2,2}
            };
            List<List<int>> rowList = new List<List<int>>
            {
                new List<int>{3,3},
                new List<int>{3,1,4},
                new List<int>{3},
                new List<int>{3},
                new List<int>{1,1},
                new List<int>{1,2,2},
                new List<int>{3,3},
                new List<int>{3,4},
                new List<int>{2,3},
                new List<int>{2,3}
            };

            _jps.CellGrid = new CellGrid(colList, rowList);

            RowHeaderGrid.ShowGridLines = true;
            ColumnHeaderGrid.ShowGridLines = true;
            CellsGrid.ShowGridLines = true;

            //Row headers
            for (int cols = 0; cols < _jps.CellGrid.RowHeaders.Max(rowHeader => rowHeader.Values.Count); cols++)
            {
                RowHeaderGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });
            }

            for (int rows = 0; rows < _jps.CellGrid.RowHeaders.Count; rows++)
            {
                RowHeaderGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });
            }

            for (int row = 0; row < _jps.CellGrid.RowHeaders.Count; row++)
            {
                Row rowHeader = _jps.CellGrid.RowHeaders[row];
                int visualCol = RowHeaderGrid.ColumnDefinitions.Count - 1;
                for (int col = rowHeader.Values.Count -1; col >= 0; col--)
                {
                    RowValue rowValue = rowHeader.Values[col];
                    Label rowHeaderValueLabel = new Label();
                    rowHeaderValueLabel.Content = rowValue.Value;
                    rowHeaderValueLabel.SetValue(Grid.RowProperty, row);
                    rowHeaderValueLabel.SetValue(Grid.ColumnProperty, visualCol--);
                    RowHeaderGrid.Children.Add(rowHeaderValueLabel);
                }

            }

            //Column headers
            for (int cols = 0; cols < _jps.CellGrid.RowHeaders.Count; cols++)
            {
                ColumnHeaderGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });
            }

            for (int rows = 0; rows < _jps.CellGrid.ColumnHeaders.Max(rowHeader => rowHeader.Values.Count); rows++)
            {
                ColumnHeaderGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });
            }

            for (int col = 0; col < _jps.CellGrid.ColumnHeaders.Count; col++)
            {
                Column colHeader = _jps.CellGrid.ColumnHeaders[col];
                int visualRow = ColumnHeaderGrid.RowDefinitions.Count - 1;
                for (int row = colHeader.Values.Count - 1; row >= 0; row--)
                {
                    ColumnValue columnValue = colHeader.Values[row];
                    Label rowHeaderValueLabel = new Label();
                    rowHeaderValueLabel.Content = columnValue.Value;
                    rowHeaderValueLabel.SetValue(Grid.RowProperty, visualRow--);
                    rowHeaderValueLabel.SetValue(Grid.ColumnProperty, col);
                    ColumnHeaderGrid.Children.Add(rowHeaderValueLabel);
                }
            }

            //Cells
            for (int cols = 0; cols < _jps.CellGrid.ColumnCount; cols++)
            {
                CellsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });
            }

            for (int rows = 0; rows < _jps.CellGrid.RowCount; rows++)
            {
                CellsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });
            }

            Rectangles = new Rectangle[_jps.CellGrid.RowCount][];
            for (int row = 0; row < _jps.CellGrid.RowCount; row++)
            {
                Rectangles[row] = new Rectangle[_jps.CellGrid.ColumnCount];
                for (int col = 0; col < _jps.CellGrid.ColumnCount; col++)
                {
                    Rectangles[row][col] = new Rectangle();
                    Rectangles[row][col].Fill = _brushes[CellValue.Unknown];
                    Rectangles[row][col].SetValue(Grid.RowProperty, row);
                    Rectangles[row][col].SetValue(Grid.ColumnProperty, col);
                    CellsGrid.Children.Add(Rectangles[row][col]);
                }
            }

            var timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 250);
            timer.Tick += FillInGrid;

            Task.Factory.StartNew(() =>
            {
                timer.Start();
                _jps.AnalysePuzzle();
                //timer.Stop();
            });

            //Final Draw
            FillInGrid(null, null);
        }

        private void FillInGrid(object sender, EventArgs eventArgs)
        {
            var output = _jps.CellGrid.Cells;
            for (int row = 0; row < _jps.CellGrid.RowCount; row++)
            {
                for (int col = 0; col < _jps.CellGrid.ColumnCount; col++)
                {
                    Rectangles[row][col].Fill = _brushes[output[row][col]];
                }
            }
        }
    }
}
