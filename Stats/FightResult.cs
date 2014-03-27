using System;
using System.Collections.Generic;
using System.Linq;
using ReversiLab.Rules;

namespace ReversiLab.Stats
{
    public class FightResult
    {
        private readonly List<GameResult> _results;

        public FightResult()
        {
            _results = new List<GameResult>();
        }

        public int TotalBlackDisks
        {
            get { return _results.Sum(i => i.BlackDisks); }
        }

        public int TotalWhiteDisks
        {
            get { return _results.Sum(i => i.WhiteDisks); }
        }

        public int TotalBlackWins
        {
            get { return TotalWins(GameService.BlackPlayer); }
        }

        public int TotalWhiteWins
        {
            get { return TotalWins(GameService.WhitePlayer); }
        }

        public void AddResult(GameResult result)
        {
            _results.Add(result);
        }

        private int TotalWins(int player)
        {
            return _results.Count(result => result.Winner == player);
        }

        private int WinnerAverage
        {
            get {return Math.Abs(TotalBlackDisks - TotalWhiteDisks); }
        }

        public void Print()
        {
            Console.WriteLine(@"Black Wins: " + TotalBlackWins + @"	Disks: " + TotalBlackDisks);
            Console.WriteLine(@"White Wins: " + TotalWhiteWins + @"	Disks: " + TotalWhiteDisks);
            Console.WriteLine(@"Winner Avg  :" + WinnerAverage);
        }
    }
}