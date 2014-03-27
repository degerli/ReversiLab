using ReversiLab.AI;
using ReversiLab.Rules;

namespace ReversiLab.Play
{
    public class ReversiPlayer
    {
        private readonly int _color;
        private IReversiStrategy _strategy;

        public ReversiPlayer(int color, IReversiStrategy strategy)
        {
            _color = color;
            _strategy = strategy;
        }

        public int Color
        {
            get { return _color; }
        }

        public IReversiStrategy Strategy
        {
            get { return _strategy; }
            set { _strategy = value; }
        }

        public string GetNextMove(Game game)        
        {
            //if (game.CurrentPlayer != Color)
            //{
            //    throw new WrongOrderException();
            //}
            if (game.AvailableMoves != null)
            {
                return _strategy.GetNextMove(game);
            }

            return null;
        }

        public override string ToString()
        {
            return Color == GameService.BlackPlayer ? "Black" : "White";
        }
    }
}