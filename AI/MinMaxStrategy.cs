using ReversiLab.Graph;
using ReversiLab.Rules;

namespace ReversiLab.AI
{
    public class MinMaxStrategy : IReversiStrategy
    {
        private readonly MinMax _minMax;

        public MinMaxStrategy(IBoardStateEvaluator evaluator, int depth, int player)
        {
            _minMax = new MinMax(evaluator, player, depth);
        }


        public string GetNextMove(Game game)
        {
            return _minMax.GetBestMove(game);
        }
    }
}