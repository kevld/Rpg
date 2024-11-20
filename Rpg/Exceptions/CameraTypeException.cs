using System;

namespace Rpg.Exceptions
{
    public class CameraTypeException : Exception
    {
        public CameraTypeException()
        {
        }

        public CameraTypeException(string message)
            : base(message)
        {
        }
    }
}
