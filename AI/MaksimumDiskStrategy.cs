using System;
using System.Collections.Generic;
using ReversiLab.Rules;

namespace ReversiLab.AI
{
    internal class MaksimumDiskStrategy : IReversiStrategy
    {
        public string GetNextMove(Game game)
        {
            IList<String> availableMoves = game.AvailableMoves;

            IBoardStateEvaluator evaluator = new ScoreAndCornersEvaluator();

            int max = 0;
            int maxIndex = 0;
            for (int i = 0; i < availableMoves.Count; i++)
            {
                Game pGame = GameService.Generate(game, availableMoves[i], true);
                int value = evaluator.Evaluate(pGame.BoardState, pGame.CurrentPlayer);
                if (value > max)
                {
                    max = value;
                    maxIndex = i;
                }
            }
            string nextMove = availableMoves[maxIndex];
            return nextMove;
        }
    }
}