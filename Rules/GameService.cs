using System;
using System.Collections.Generic;
using System.Linq;
using ReversiLab.Exceptions;

namespace ReversiLab.Rules
{
    public static class GameService
    {
        //~ --- [STATIC FIELDS/INITIALIZERS] -------------------------------------------------------------------------------

        private const int EndOfDirection = -1;

        // Board square states
        public const int EmptyPlace = 0;
        public const int BlackDisk = 1;
        public const int WhiteDisk = 2;

        // Players
        public const int NoPlayer = 0;
        public const int BlackPlayer = 1;
        public const int WhitePlayer = 2;

        private static int _size=8;

        public static Game Generate(Game game, string move, bool leafNode)
        {
            var newGame = new Game(game);
            Move(newGame, move, newGame.CurrentPlayer, leafNode);
            return newGame;
        }


        public static void Move(Game game, string piece, int player, bool leafNode = false)
        {
            int currentPlayer = game.CurrentPlayer;

            if (player != currentPlayer)
            {
                throw new WrongOrderException();
            }

            var position = new Position(piece);
            
            IDictionary<Position, List<Path>> playerPaths = FindAvailablePaths(game, position, currentPlayer);

            if (playerPaths == null || playerPaths.Count == 0)
            {
                throw new IllegalMoveException();
            }

            OccupyPaths(game, playerPaths, position);

            if (leafNode)
            {
                return;
            }

            int otherPlayer = currentPlayer == BlackPlayer ? WhitePlayer : BlackPlayer;

            // Are there any legal moves for other color
            if ((playerPaths = FindAllAvailablePaths(game, otherPlayer)).Count != 0)
            {
                game.CurrentPlayer = otherPlayer;
                game.AvailableMoves = PositionSet2StringList(playerPaths.Keys);
            }
            // if have legal moves for current Color
            else if ((playerPaths = FindAllAvailablePaths(game, currentPlayer)).Count != 0)
            {
                game.AvailableMoves = PositionSet2StringList(playerPaths.Keys);
            }
            else
            {
                game.Started = false;
                game.CurrentPlayer = NoPlayer;
                game.AvailableMoves = null;
            }
        }

        //~ ----------------------------------------------------------------------------------------------------------------

        private static IDictionary<Position, List<Path>> FindAllAvailablePaths(Game game, int player)
        {
            var availablePaths = new Dictionary<Position, List<Path>>();

            for (int row = 0; row < _size; row++)
            {
                for (int col = 0; col < _size; col++)
                {
                    var position = new Position(row, col);
                    IDictionary<Position, List<Path>> positionPaths = FindAvailablePaths(game, position, player);

                    if (positionPaths != null && positionPaths.Count != 0)
                    {
                        availablePaths = availablePaths.Concat(positionPaths).ToDictionary(x => x.Key, x => x.Value);
                    }
                }
            }

            return availablePaths;
        }


        //~ ----------------------------------------------------------------------------------------------------------------

        /**
     * Scans all directions for valid pattern. Valid pattern:
     *
     * <pre>
     [SAME COLOR] [DIFF. COLOR] ... [DIFF. COLOR] [TARGET]
     * </pre>
     *
     * pattern rules:
     *
     * <ol>
     * <li>Target location cannot be an empty place.</li>
     * <li>Location before target location should be in different color according to current Color</li>
     * <li>Pattern should start with same color according to current Color</li>
     * </ol>
     *
     * @param   game      current game
     * @param   position  target position
     * @param   Color    Color value for pattern validation
     *
     * @return  Returns found path if valid pattern found.
     */

        public static IDictionary<Position, List<Path>> FindAvailablePaths(Game game, Position position, int player)
        {
            IList<List<int>> boardState = game.BoardState;

            if (boardState[position.Row][position.Col] != EmptyPlace)
            {
                return null;
            }

            IDictionary<Position, List<Path>> playerPaths = new Dictionary<Position, List<Path>>();
            int differentColor = player == WhitePlayer ? BlackDisk : WhiteDisk;
            int sameColor = player; // Redundant constant for code readability

            for (int direction = 0; direction < 8; direction++)
            {
                int value = GetPositionValue(game, position, direction, 1);

                if (value == differentColor)
                {
                    for (int step = 2; step < 8; step++)
                    {
                        value = GetPositionValue(game, position, direction, step);

                        if (value == EndOfDirection || value == EmptyPlace)
                        {
                            break;
                        }
                        if (value == sameColor)
                        {
                            List<Path> paths = null;
                            if (playerPaths.ContainsKey(position))
                            {
                                paths = playerPaths[position];
                            }

                            if (paths == null)
                            {
                                paths = new List<Path>();
                            }

                            paths.Add(new Path(position, direction, step));

                            if (!playerPaths.ContainsKey(position))
                            {
                                playerPaths.Add(position, paths);
                            }


                            break;
                        }
                    } // end for
                }
            } // end for

            return playerPaths;
        }

        /**
 * There are 8 legal directions for Color move. This method returns desired position value for given step and
 * directions and also returns -1 for out of bound locations.
 *
 * <pre>
 c   d   e   f
 4  [2] [0] [0] [1]  >>> direction 2
 * </pre>
 *
 * If start position is c4 and step is 3 then target location is f4 and the value is 1.
 *
 * @param   position   start position
 * @param   direction  direction number. Range 0 - 7
 * @param   step       translation distance
 *
 * @return  -1 for out of bound locations, 0 for empty locations, 1 for black disk, 2 for white disk
 */

        private static int GetPositionValue(Game game, Position position, int direction, int step)
        {
            Position translatedPosition = GetTranslatedPosition(position, direction, step);
            if (translatedPosition == null)
            {
                return EndOfDirection;
            }
            IList<List<int>> boardState = game.BoardState;
            return boardState[translatedPosition.Row][translatedPosition.Col];
        }


        /**
     * Returns translated position for required direction. If direction is other than range 0 - 7 then returns base
     * location.
     *
     * <pre>
     c   d   e   f
     4  [2] [0] [0] [1]  >>> direction 2
     * </pre>
     *
     * If start position is c4 and step is 3 then target location is f4. Returns [3, 5]
     *
     * @param   position   start position
     * @param   direction  direction number. Range 0 - 7
     * @param   step       translation distance
     *
     * @return  returns translated position index 0 is row, index 1 is col
     *
     * @throws  OutOfBoundsException
     */

        private static Position GetTranslatedPosition(Position position, int direction, int step)
        {
            int row = position.Row;
            int col = position.Col;

            switch (direction)
            {
                case 0:
                    row  -= step;
                    break;
                case 1:
                    row  -= step;
                    col  += step;
                    break;
                case 2:
                    col  += step;
                    break;
                case 3:
                    row += step;
                    col += step;
                    break;
                case 4:
                    row += step;
                    break;
                case 5:
                    row += step;
                    col -= step;
                    break;
                case 6:
                    col -= step;
                    break;
                case 7:
                    row -= step;
                    col -= step;
                    break;
            }

            if (row < 0 || col < 0 || row > _size - 1 || col > _size - 1)
            {
                return null;
            }

            return new Position(row, col);
        }


        //~ ----------------------------------------------------------------------------------------------------------------


        //~ ----------------------------------------------------------------------------------------------------------------

        private static List<String> PositionSet2StringList(IEnumerable<Position> positionSet)
        {
            var keyList = new List<String>();
            keyList.AddRange(positionSet.Select(e => e.ToString()));
            return keyList;
        }

        //~ ----------------------------------------------------------------------------------------------------------------

        /**
     * Occupies path for current Color
     *
     * <pre>
     c   d   e   f   g
     4  [2] [1] [1] [0] [0] >>> direction 2
     * </pre>
     *
     * After occupation: (Direction: 2, Step: 3)
     *
     * <pre>
     c   d   e   f   g
     4  [2] [2] [2] [2] [0]  >>> direction 2
     * </pre>
     *
     * @param  path  found path
     */

        public static void OccupyPath(Game game, Path path)
        {
            OccupyPath(game, path.Position, path.Direction, path.Step);
        }


        //~ ----------------------------------------------------------------------------------------------------------------

        /**
     * Occupies path for current Color
     *
     * <pre>
     c   d   e   f   g
     4  [2] [1] [1] [0] [0] >>> direction 2
     * </pre>
     *
     * After occupation: (Direction: 2, Step: 3)
     *
     * <pre>
     c   d   e   f   g
     4  [2] [2] [2] [2] [0]  >>> direction 2
     * </pre>
     *
     * @param  position   start position
     * @param  direction  direction number. Range 0 - 7
     * @param  step       translation distance
     */

        public static void OccupyPath(Game game, Position position, int direction, int step)
        {
            IList<List<int>> boardState = game.BoardState;
            int currentPlayer = game.CurrentPlayer;

            for (int i = 0; i < step; i++)
            {
                Position translatedPosition = GetTranslatedPosition(position, direction, i);

                if (translatedPosition == null)
                {
                    break;
                }
                // Color constants and disk constants are equivalent. So can be used "currentPlayer" for
                // current Color's disk color.
                boardState[translatedPosition.Row][translatedPosition.Col] = currentPlayer;
            }
        }


        //~ ----------------------------------------------------------------------------------------------------------------

        public static void OccupyPaths(Game game, IDictionary<Position, List<Path>> playerPaths, Position position)
        {
            if (playerPaths != null && playerPaths.Count != 0)
            {
                List<Path> foundPaths = playerPaths[position];

                if (foundPaths != null && foundPaths.Count != 0)
                {
                    foreach (Path path in foundPaths)
                    {
                        OccupyPath(game, path);
                    }
                }
                //else
                //{
                //    throw new IllegalMoveException();
                //}
            }
        }


        //~ ----------------------------------------------------------------------------------------------------------------

        public static void Start(Game game, int boardSize)
        {
            switch (boardSize)
            {
                case 4:
                    Start4X4(game);
                    break;
                case 8:
                    Start8X8(game);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(@"boardSize must be either 4 or 8");
            }
        }

        public static void Start8X8(Game game)
        {
            _size = 8;
            if (game.Started)
            {
                throw new AlreadyStartedException();
            }

            game.Started = true;
            game.CurrentPlayer = BlackPlayer;
            game.BoardState = new List<List<int>>(new[]
                //                   0  1  2  3  4  5  6  7
                //                   a  b  c  d  e  f  g  h
            {
                new List<int>(new[] {0, 0, 0, 0, 0, 0, 0, 0}), // 1 0
                new List<int>(new[] {0, 0, 0, 0, 0, 0, 0, 0}), // 2 1
                new List<int>(new[] {0, 0, 0, 0, 0, 0, 0, 0}), // 3 2
                new List<int>(new[] {0, 0, 0, 2, 1, 0, 0, 0}), // 4 3
                new List<int>(new[] {0, 0, 0, 1, 2, 0, 0, 0}), // 5 4
                new List<int>(new[] {0, 0, 0, 0, 0, 0, 0, 0}), // 6 5
                new List<int>(new[] {0, 0, 0, 0, 0, 0, 0, 0}), // 7 6
                new List<int>(new[] {0, 0, 0, 0, 0, 0, 0, 0}) // 8 7
            });

            game.AvailableMoves = new List<string>(new[] {"c4", "d3", "e6", "f5"});
        }


        public static void Start4X4(Game game)
        {
            _size = 4;
            if (game.Started)
            {
                throw new AlreadyStartedException();
            }

            game.Started = true;
            game.CurrentPlayer = BlackPlayer;
            game.BoardState = new List<List<int>>(new[]
            {
                //                    0  1  2  3
                //                    a  b  c  d
                new List<int>(new[] {0, 0, 0, 0}), // 1 0
                new List<int>(new[] {0, 2, 1, 0}), // 2 1
                new List<int>(new[] {0, 1, 2, 0}), // 3 2
                new List<int>(new[] {0, 0, 0, 0}) // 4 3
            });

            game.AvailableMoves = new List<string>(new[] {"a2", "b1", "c4", "d3"});
        }
    }
}