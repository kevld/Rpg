using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
