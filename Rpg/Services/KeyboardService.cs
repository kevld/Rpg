using Microsoft.Xna.Framework.Input;
using Rpg.EventsArgsModels;
using Rpg.Interfaces;
using System;
using System.Collections.Generic;

namespace Rpg.Services
{
    public class KeyboardService : IKeyboardService
    {

        public event EventHandler KeyboardEvent;

        public void SendKeyEvent(Keys key, bool isPressed)
        {
            KeyboardEvent?.Invoke(this, new KeyboardEventArgs()
            {
                Keys = key,
                IsPressed = isPressed
            });
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                KeyboardEvent = null;
            }
        }
    }
}
