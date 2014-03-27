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
             minmax25= new MinMax(new ScoreAndCornersEvaluator(), player, 5);
             minmax3 = new MinMax(new ScoreAndCornersEvaluator(), player, 6);
        }

        private MinMax minmax25;
        private MinMax minmax3;
        private MinMax minmax35;
        private MinMax minMax4;

        public string GetNextMove(Game game)
        {
            if (game.AvailableMoves.Count < 6)
            {
                return minmax3.GetBestMove(game);
            }
            return minmax25.GetBestMove(game);
        }
    }
}
