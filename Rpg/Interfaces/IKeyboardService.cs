using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Rpg.Interfaces
{
    public interface IKeyboardService : IDisposable
    {
        public event EventHandler KeyboardEvent;

        public void SendKeyEvent(Keys key, bool isPressed);
    }
}
