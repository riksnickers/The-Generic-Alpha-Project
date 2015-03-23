using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TheJapanesePuzzleSolver;

namespace JapanesePuzzleSolverClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            JapanesePuzzleSolver jps = new JapanesePuzzleSolver();

            List<List<int>> colList = new List<List<int>>
            {
                new List<int>{10},
                new List<int>{10},
                new List<int>{10},
                new List<int>{10},
                new List<int>{10},
                new List<int>{10},
                new List<int>{10},
                new List<int>{10},
                new List<int>{10},
                new List<int>{10}
            };
            List<List<int>> rowList = new List<List<int>>
            {
                new List<int>{10},
                new List<int>{7,2},
                new List<int>{8,1},
                new List<int>{1,1,1,1,2},
                new List<int>{10}
            };

            jps.Grid = new Grid(colList, rowList);

            var output = jps.AnalysePuzzle();

            for (int i = 0; i < output.Length; i++)
            {
                for (int j = 0; j < output[i].Length; j++)
                {
                    WriteAt(i,j, output[i][j]);
                }
            }
            
            Console.ReadLine();
        }

        private static void WriteAt(int row, int col, CellValue content)
        {
            Console.SetCursorPosition(col, row);
            switch (content)
            {
                case CellValue.Empty: Console.Write('E'); break;
                case CellValue.FilledIn: Console.Write('X'); break;
                case CellValue.Overlap: Console.Write('O'); break;
                default: Console.Write('?'); break;
            }
        }    
    }
}
