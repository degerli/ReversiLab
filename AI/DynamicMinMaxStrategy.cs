using ReversiLab.Graph;
using ReversiLab.Rules;

namespace ReversiLab.AI
{
    class DynamicMinMaxStrategy : IReversiStrategy
    {

        private int player;

        public DynamicMinMaxStrategy(int player)
        {
            this.player = player;
             _minmax25= new MinMax(new ScoreAndCornersEvaluator(), player, 5);
             _minmax3 = new MinMax(new ScoreAndCornersEvaluator(), player, 6);
        }

        private readonly MinMax _minmax25;
        private readonly MinMax _minmax3;
       

        public string GetNextMove(Game game)
        {
            if (game.AvailableMoves.Count < 6)
            {
                return _minmax3.GetBestMove(game);
            }
            return _minmax25.GetBestMove(game);
        }
    }
}
