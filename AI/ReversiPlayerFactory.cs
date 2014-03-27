using ReversiLab.Play;

namespace ReversiLab.AI
{
    public enum PlayerType { DynamicMinmax, Minimax2WithoutCorners, MiniMax1, MiniMax2, MiniMax3, MiniMax25, Random, AlwaysFirst }

    public static class ReversiPlayerFactory
    {
        public static ReversiPlayer Create(PlayerType type, int color)
        {
            switch (type)
            {
                case PlayerType.MiniMax1:
                    return new ReversiPlayer(color, new MinMaxStrategy(new ScoreAndCornersEvaluator(), 2, color));
                    
                case PlayerType.MiniMax2:
                    return new ReversiPlayer(color, new MinMaxStrategy(new ScoreAndCornersEvaluator(), 4, color));
                    
                case PlayerType.MiniMax25:
                    return new ReversiPlayer(color, new MinMaxStrategy(new ScoreAndCornersEvaluator(), 5, color));

                case PlayerType.MiniMax3:
                    return new ReversiPlayer(color, new MinMaxStrategy(new ScoreAndCornersEvaluator(), 6, color));
                    
                case PlayerType.Random:
                    return new ReversiPlayer(color, new RandomStrategy());

                case PlayerType.AlwaysFirst:
                    return new ReversiPlayer(color, new AlwaysFirstStrategy());

                case PlayerType.Minimax2WithoutCorners:
                    return new ReversiPlayer(color, new MinMaxStrategy(new ScoreEvaluator(), 2, color));
                case PlayerType.DynamicMinmax:
                    return new ReversiPlayer(color, new DynamicMinMaxStrategy(color));
                default:
                    return null;
            }
        }

    }
}
