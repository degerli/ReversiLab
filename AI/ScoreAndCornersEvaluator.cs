using System.Collections.Generic;
using System.Linq;
using ReversiLab.Rules;

namespace ReversiLab.AI
{
    public class ScoreAndCornersEvaluator : IBoardStateEvaluator
    {
        public int Evaluate(IEnumerable<List<int>> boardState, int player)
        {
            return EvaluateCorners(boardState, player);
        }

        private static readonly string[] Corners = { "a1", "h1", "a8", "h8" };

        private static readonly Dictionary<string, string[]> Neighbours = new Dictionary<string, string[]>
        {
            { "a1", new[]{"a2", "b1", "b2"}},
            { "h1", new[]{"h2", "g1", "g2"}},
            { "a8", new[]{"a7", "b8", "b7"}},
            { "h8", new[]{"h7", "g8", "g7"}},
        };

        private string IsNextToCorner(string pos)
        {
            foreach (var pair in Neighbours)
            {
                if (pair.Value.Contains(pos))
                {
                    return pair.Key;
                }
            }
            return null;
        }

        private bool OnCorner(int row, int col)
        {
            if ((row == 0 && col == 0) || (row == 7 && col == 7) || (row == 0 && col == 7) || (row == 0 && col == 7))
            {
                return true;
            }
            
            return false;
        }

        private int EvaluateCorners(IEnumerable<List<int>> boardState, int player)
        {
            int otherPlayer = player == GameService.BlackPlayer ? GameService.WhitePlayer : GameService.BlackPlayer;
            var state = boardState as IList<List<int>> ?? boardState.ToList();
            int value = 0;
            for (int i = 0; i < state.Count(); i++)
            {
                for (int j = 0; j < state[i].Count; j++)
                {
                    if (OnCorner(i, j))
                    {
                        if (state[i][j] == player)
                        {
                            value += 10;
                            continue;
                        }
                        if (state[i][j] == otherPlayer)
                        {
                            value -= 10;
                            continue;
                        }
                    }
                    if (state[i][j] == player)
                    {
                        value++;
                    }
                }
            }
            return value;
        }

        
    }
}