using System.Text;

namespace ReversiLab.Rules
{
    public class Position
    {
        //~ --- [STATIC FIELDS/INITIALIZERS] -------------------------------------------------------------------------------
        public const string BoardLetters = "abcdefgh";
        public const string BoardNumbers = "12345678";

        //~ --- [INSTANCE FIELDS] ------------------------------------------------------------------------------------------

        private readonly int _col;
        private readonly int _row;


        public Position(int row, int col)
        {
            _col = col;
            _row = row;
        }

        public Position(string piece)
        {
            //char[] chars = piece.ToCharArray();
            _col = BoardLetters.IndexOf(piece[0]);
            _row = BoardNumbers.IndexOf(piece[1]);
        }

        public int Col
        {
            get { return _col; }
        }

        public int Row
        {
            get { return _row; }
        }

        public static string PosToString(int row, int col)
        {
            var pos = new char[2];
            pos[0] = BoardLetters[col];
            pos[1] = BoardNumbers[row];
            return new string(pos);
        }

        public override string ToString()
        {
            var text = new StringBuilder();
            text.Append(BoardLetters[_col]);
            text.Append(_row + 1);
            return text.ToString();
        }
    }
}