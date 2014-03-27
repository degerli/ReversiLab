namespace ReversiLab.Play
{
    /// <summary>
    ///     Abstract Handler in chain of responsibility Pattern
    /// </summary>
    public abstract class Logger
    {
        private readonly Verbosity _logMask;

        // The next Handler in the chain
        private Logger _next;

        public Logger(Verbosity mask)
        {
            _logMask = mask;
        }

        /// <summary>
        ///     Sets the Next logger to make a list/chain of Handlers
        /// </summary>
        public Logger SetNext(Logger nextlogger)
        {
            _next = nextlogger;
            return nextlogger;
        }

        public void Message(string msg, Verbosity severity)
        {
            if ((severity & _logMask) != 0)
            {
                WriteMessage(msg);
            }
            if (_next != null)
            {
                _next.Message(msg, severity);
            }
        }

        protected abstract void WriteMessage(string msg);
    }
}