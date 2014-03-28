using System;
using System.Collections.Generic;
using ReversiLab.Rules;

namespace ReversiLab.AI
{
    /// <summary>
    /// Chooses a move in available moves randomly
    /// </summary>
    internal class RandomStrategy : IReversiStrategy
    {
        private readonly Random _random;

        public RandomStrategy()
        {
            _random = new Random();
        }


        public string GetNextMove(Game game)
        {
            IList<String> availableMoves = game.AvailableMoves;
            int randomInt = _random.Next(availableMoves.Count);
            string nextMove = availableMoves[randomInt];
            return nextMove;
        }
    }
}