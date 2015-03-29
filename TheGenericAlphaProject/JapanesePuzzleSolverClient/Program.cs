using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
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

            jps.CellGrid = new CellGrid(colList, rowList);

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
