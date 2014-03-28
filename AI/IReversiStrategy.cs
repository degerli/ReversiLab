using ReversiLab.Rules;

namespace ReversiLab.AI
{
    public interface IReversiStrategy
    {
        string GetNextMove(Game game);       
    }
}