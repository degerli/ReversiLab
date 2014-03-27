namespace ReversiLab.Rules
{
    public class Path
    {
        private readonly Position _position;
        private readonly int _direction;
        private readonly int _step;

        public Path(Position position, int direction, int step)
        {
            _direction = direction;
            _position = position;
            _step = step;
        }


        public int Direction
        {
            get { return _direction; }
        }

        public Position Position
        {
            get { return _position; }
        }

        public int Step
        {
            get { return _step; }
        }
    }
}