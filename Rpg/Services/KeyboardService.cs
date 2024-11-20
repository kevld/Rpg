using Microsoft.Xna.Framework.Input;
using Rpg.Core.Services;
using Rpg.Core.Services.Interfaces;
using Rpg.EventsArgsModels;
using System;

namespace Rpg.Services
{
    public class KeyboardService : BaseService, IKeyboardService
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

        protected override void CleanIfDisposing()
        {
            KeyboardEvent = null;
        }
    }
}
