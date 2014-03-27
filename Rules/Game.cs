using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReversiLab.Rules
{
    public class Game
    {
        private const string RowHeader = "  a b c d e f g h" + "\n";

        /// <summary>
        ///     To create a new copy of a game object.
        /// </summary>
        /// <param name="game"></param>
        public Game(Game game)
        {
            BoardState = game.BoardState.Select(list => list.ToList()).ToList();
            AvailableMoves = game.AvailableMoves.ToList();
            CurrentPlayer = game.CurrentPlayer;
        }

        public Game()
        {
        }

        public List<List<int>> BoardState { get; set; }

        public List<string> AvailableMoves { get; set; }

        public int CurrentPlayer { get; set; }

        public bool Started { get; set; }

        public int BoardStateValue { get; set; }

        public string Id { get; set; }

        public override string ToString()
        {
            return BoardState.ToString();
        }

        public string AvailableMovesToString()
        {
            StringBuilder text = new StringBuilder().Append("{");

            if (AvailableMoves != null)
                foreach (string move in AvailableMoves)
                {
                    text.Append(move).Append(", ");
                }
            text.Append("}");
            return text.ToString();
        }


        public string StateToString()
        {
            StringBuilder text = new StringBuilder().Append(RowHeader);

            for (int row = 0, rowValue = 1; row < BoardState.Count; row++, rowValue++)
            {
                text.Append(rowValue).Append(" ");

                for (int col = 0; col < BoardState.Count; col++)
                {
                    int currentPlaceValue = BoardState[row][col];

                    if (currentPlaceValue == GameService.EmptyPlace)
                    {
                        text.Append("- ");
                    }
                    else if (currentPlaceValue == GameService.WhiteDisk)
                    {
                        text.Append("O ");
                    }
                    else if (currentPlaceValue == GameService.BlackDisk)
                    {
                        text.Append("X ");
                    }
                }

                text.Append(rowValue).Append("\n");
            }

            return text.Append(RowHeader).ToString();
        }

        public void PrintState()
        {
            Console.WriteLine(StateToString());
        }

        public void PrintAvailableMoves()
        {
            Console.WriteLine(AvailableMovesToString());
        }

        public int GetScore(int player)
        {
            return BoardState.SelectMany(list => list).Count(i => i == player);
        }
    }
}