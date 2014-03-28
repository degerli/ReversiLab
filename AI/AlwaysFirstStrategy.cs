using System.Linq;
using ReversiLab.Rules;

namespace ReversiLab.AI
{
    /// <summary>
    /// Always plays the first available move
    /// </summary>
    internal class AlwaysFirstStrategy : IReversiStrategy
    {
        public string GetNextMove(Game game)
        {
            return game.AvailableMoves.First();
        }
    }
}