using System;
using System.Collections.Generic;

namespace ReversiLab.AI
{
    internal class PositionEvaluator : IBoardStateEvaluator
    {
        private static readonly IDictionary<string, int> _pValues = new Dictionary<string, int>
        {
            {"a1", 99},
            {"h1", 99},
            {"a8", 99},
            {"h8", 99},
            {"b1", -8},
            {"g1", -8},
            {"g8", -8},
            {"b8", -8},
            {"a2", -8},
            {"a7", -8},
            {"h2", -8},
            {"h7", -8},
            {"b2", -24},
            {"b7", -24},
            {"g2", -24},
            {"g7", -24},
            {"c1", 8},
            {"c8", 8},
            {"f1", 8},
            {"f8", 8},
            {"a3", 8},
            {"a6", 8},
            {"h3", 8},
            {"h6", 8},
            {"c2", -4},
            {"b3", -4},
            {"f2", -4},
            {"g3", -4},
            {"b6", -4},
            {"c7", -4},
            {"g6", -4},
            {"f7", -4},
            {"c3", 7},
            {"f3", 7},
            {"c6", 7},
            {"f6", 7},
            {"d1", 6},
            {"d8", 6},
            {"e1", 6},
            {"e8", 6},
            {"a4", 6},
            {"a5", 6},
            {"h4", 6},
            {"h5", 6},
            {"d2", -3},
            {"e2", -3},
            {"d7", -3},
            {"e7", -3},
            {"b4", -3},
            {"b5", -3},
            {"g4", -3},
            {"g5", -3},
            {"d3", 4},
            {"e3", 4},
            {"d6", 4},
            {"e6", 4},
            {"c4", 4},
            {"c5", 4},
            {"f4", 4},
            {"f5", 4},
        };

        private string[] _corners = {"a1", "h1", "a8", "h8"};

        public int Evaluate(IEnumerable<List<int>> boardState, int player)
        {
            throw new NotImplementedException();
        }

        public static void updateCornerValues(string position)
        {
            switch (position)
            {
                case "a1":
                    _pValues.Add("b1", 50);
                    _pValues.Add("a2", 50);
                    break;
                case "a8":
                    _pValues.Add("a7", 50);
                    _pValues.Add("b8", 50);
                    break;
                case "h1":
                    _pValues.Add("g1", 50);
                    _pValues.Add("h2", 50);
                    break;
                case "h8":
                    _pValues.Add("h7", 50);
                    _pValues.Add("g8", 50);
                    break;
            }
        }
    }
}