using Microsoft.Xna.Framework.Input;
using System;

namespace Rpg.Core.Services.Interfaces
{
    public interface IKeyboardService : IBaseService
    {
        public event EventHandler KeyboardEvent;

        public void SendKeyEvent(Keys key, bool isPressed);
    }
}
