using System.Collections.Generic;
using System.Linq;

namespace ReversiLab.AI
{
    public class ScoreEvaluator : IBoardStateEvaluator
    {
        public int Evaluate(IEnumerable<List<int>> boardState, int player)
        {
            
            return boardState.SelectMany(list => list).Count(i => i == player);

        }
        
    }
}