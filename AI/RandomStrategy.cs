using System;
using System.Collections.Generic;
using ReversiLab.Rules;

namespace ReversiLab.AI
{
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