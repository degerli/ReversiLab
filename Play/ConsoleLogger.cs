using System;

namespace ReversiLab.Play
{
    public class ConsoleLogger : Logger
    {
        public ConsoleLogger(Verbosity mask)
            : base(mask)
        {
        }

        protected override void WriteMessage(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}