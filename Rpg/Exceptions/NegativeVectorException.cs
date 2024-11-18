using System;

namespace Rpg.Exceptions
{
    public class NegativeVectorException : Exception
    {
        public NegativeVectorException()
        {
        }

        public NegativeVectorException(string message)
            : base(message)
        {
        }
    }
}
