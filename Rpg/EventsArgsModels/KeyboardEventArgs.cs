using Microsoft.Xna.Framework.Input;
using System;

namespace Rpg.EventsArgsModels
{
    public class KeyboardEventArgs : EventArgs
    {
        public Keys Keys { get; set; }

        public bool IsPressed { get; set; }
    }
}
