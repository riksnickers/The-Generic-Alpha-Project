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
        public Grid Grid { get; set; }

        public List<IRule> Rules { get; set; }

        public CellValue[][] AnalysePuzzle()
        {
            Rules = new List<IRule>();
            Rules.Add(new FullRowRule());

            foreach (IRule rule in Rules)
            {
                var ruleOutcome = rule.ApplyRule(Grid);
                Grid.Cells = ruleOutcome;
            }
            return Grid.Cells;
        }

        public bool[][] Analyse()
        {
            var cells = new bool[Grid.RowCount][];
            for (int row = 0; row < Grid.RowCount; row++)
            {
                cells[row] = Grid.AnalyzeRow(row);
            }
            return cells;
        }
    }
}
