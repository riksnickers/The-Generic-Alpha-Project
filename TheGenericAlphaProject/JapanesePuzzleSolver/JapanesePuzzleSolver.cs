using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using TheJapanesePuzzleSolver.Rules;

namespace TheJapanesePuzzleSolver
{
    public class JapanesePuzzleSolver
    {
        public CellGrid CellGrid { get; set; }

        public List<IRule> Rules { get; set; }

        public CellValue[][] AnalysePuzzle()
        {
            Rules = new List<IRule>();
            Rules.Add(new FullRowRule());
            Rules.Add(new FullColumnRule());
            Rules.Add(new RowBorderRule());
            Rules.Add(new ColumnBorderRule());
            Rules.Add(new CheckCompletedRowRule());
            Rules.Add(new CheckCompletedColumnRule());

            //while (!CellGrid.Cells.IsSolved())
            //{
                foreach (IRule rule in Rules)
                {
                    var ruleOutcome = rule.ApplyRule(CellGrid);
                    CellGrid.Cells = ruleOutcome;
                }
            //}

            return CellGrid.Cells;
        }

        public bool[][] Analyse()
        {
            var cells = new bool[CellGrid.RowCount][];
            for (int row = 0; row < CellGrid.RowCount; row++)
            {
                cells[row] = CellGrid.AnalyzeRow(row);
            }
            return cells;
        }
    }
}
