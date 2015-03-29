using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheJapanesePuzzleSolver.Rules
{
    public interface IRule
    {
        CellValue[][] ApplyRule(CellGrid cellGrid);
    }
}
