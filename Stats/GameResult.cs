using System;
using ReversiLab.Rules;

namespace ReversiLab.Stats
{
    public class GameResult
    {
        public GameResult(int blackDisks, int whiteDisks)
        {
            BlackDisks = blackDisks;
            WhiteDisks = whiteDisks;
        }

        public int WhiteDisks { private set; get; }

        public int BlackDisks { private set; get; }

        public int Winner
        {
            get { return BlackDisks > WhiteDisks ? GameService.BlackPlayer : GameService.WhitePlayer; }
        }

        public int WinnerAverage
        {
            get { return Math.Abs(BlackDisks - WhiteDisks); }
        }

        public void Print()
        {
            Console.WriteLine(@"Black Player: " + BlackDisks);
            Console.WriteLine(@"White Player: " + WhiteDisks);
            Console.WriteLine(@"    Winner Avg  :" + WinnerAverage);
        }
    }
}