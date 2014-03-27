using System.Collections.Generic;

namespace ReversiLab.AI
{
    public interface IBoardStateEvaluator
    {
        int Evaluate(IEnumerable<List<int>> boardState, int player);
    }
}