namespace ReversiLab.Graph
{
    internal class GameNode
    {       
        public GameNode(string id, string move, int player)
        {
            Id = id;
            Move = move;
            Player = player;
        }


        public string Id { get; private set; }
        public string Move { get; private set; }
        public int Player { get; private set; }


        public int Value { get; set; }
        public bool Visited { get; set; }
    }
}