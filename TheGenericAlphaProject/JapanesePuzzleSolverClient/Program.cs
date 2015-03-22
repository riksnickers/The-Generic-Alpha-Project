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
                new List<int>{5},
                new List<int>{5},
                new List<int>{5},
                new List<int>{5},
                new List<int>{5}
            };
            List<List<int>> rowList = new List<List<int>>
            {
                new List<int>{5},
                new List<int>{5},
                new List<int>{5},
                new List<int>{5},
                new List<int>{5}
            };

            jps.Grid = new Grid(colList, rowList);

            var output = jps.Analyse();

            for (int i = 0; i < output.Length; i++)
            {
                for (int j = 0; j < output[i].Length; j++)
                {
                    WriteAt(i,j, output[i][j] ? 'X' : '-');
                }
            }
            
            Console.ReadLine();
        }

        private static void WriteAt(int row, int col, char content)
        {
            Console.SetCursorPosition(col, row);
            Console.Write(content);
        }    
    }
}
