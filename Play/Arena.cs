using System;
using System.Diagnostics;
using ReversiLab.Exceptions;
using ReversiLab.Rules;
using ReversiLab.Stats;

namespace ReversiLab.Play
{
    public class Arena
    {
        private readonly int _boardSize;
   
        public Arena(int boardSize)
        {
            _boardSize = boardSize;
        }

        //public Arena(Verbosity verbosity)
        //{
        //    _verbosity = verbosity;
        //    _logger = new ConsoleLogger(Verbosity.Less);
        //    _logger.SetNext(new ConsoleLogger(Verbosity.Detailed));
        //    _logger.SetNext(new ConsoleLogger(Verbosity.All));
        //}

        public FightResult MakeFight(ReversiPlayer black, ReversiPlayer white, int times)
        {
            var fightResult = new FightResult();
            for (int i = 0; i < times; i++)
            {
                var sw = new Stopwatch();
                sw.Start();
                GameResult gameResult = MakeFight(black, white, false);
                sw.Stop();
                fightResult.AddResult(gameResult);
                Console.WriteLine(@"    time:" + sw.ElapsedMilliseconds + @" ms");
            }
            return fightResult;
        }

        public GameResult MakeFight(ReversiPlayer black, ReversiPlayer white, bool v=true)
        {
            var players = new ReversiPlayer[3]; //[0] won't be used! 

            players[GameService.BlackPlayer] = black;
            players[GameService.WhitePlayer] = white;

            var game = new Game();

            GameService.Start(game, _boardSize);

            int player = GameService.BlackPlayer;
            while (game.CurrentPlayer != GameService.NoPlayer)
            {
                try
                {
                    PrintLine(game.AvailableMovesToString(), v);
                    var sw = new Stopwatch();
                    sw.Start();
                    string move = players[player].GetNextMove(game);
                    sw.Stop();
                    PrintLine(sw.ElapsedMilliseconds + " ms", v);
                    GameService.Move(game, move, player);
                    PrintLine(players[player] + @" moves: " + move, v);
                    PrintLine(game.StateToString(), v);
                    player = Switch(player);
                }
                catch (WrongOrderException)
                {
                    player = Switch(player);
                }
            }

            var result = new GameResult(game.GetScore(GameService.BlackPlayer), game.GetScore(GameService.WhitePlayer));

            result.Print();

            return result;
        }

        private void PrintLine(string msg, bool v)
        {
            if (v)
                Console.WriteLine(msg);
        }


        private static int Switch(int player)
        {
            return player == GameService.BlackPlayer ? GameService.WhitePlayer : GameService.BlackPlayer;
        }
    }
}