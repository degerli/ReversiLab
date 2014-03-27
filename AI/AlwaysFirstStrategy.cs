using System.Linq;
using ReversiLab.Rules;

namespace ReversiLab.AI
{
    internal class AlwaysFirstStrategy : IReversiStrategy
    {
        public string GetNextMove(Game game)
        {
            return game.AvailableMoves.First();
        }
    }
}