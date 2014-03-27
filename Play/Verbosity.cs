using System;

namespace ReversiLab.Play
{
    [Flags]
    public enum Verbosity
    {
        None = 0,
        Less = 1,
        Detailed = 2,
        All = 4,
        //Error = 8,
        //FunctionalMessage = 16,
        //FunctionalError = 32,
        //All = 63
    }
}