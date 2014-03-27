using ReversiLab.Rules;

namespace ReversiLab.AI
{
    public interface IReversiStrategy
    {
        string GetNextMove(Game game);

        //protected int Sign(Game game, int player)
        //{
        //    return player == game.CurrentPlayer ? 1 : -1;
        //}
    }
}