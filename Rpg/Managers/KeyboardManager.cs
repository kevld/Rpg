using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using Rpg.Helpers;
using Rpg.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rpg.Managers
{
    public class KeyboardManager : DIManager, IUpdateable
    {
        private readonly IKeyboardService _keyboardService;

        public KeyboardManager(GameServiceContainer services) : base(services)
        {
            _keyboardService = services.GetService<IKeyboardService>();
        }

        public bool Enabled => throw new NotImplementedException();

        public int UpdateOrder => throw new NotImplementedException();

        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        public void Update(GameTime gameTime)
        {
            KeyboardExtended.Update();
            KeyboardStateExtended keyboardState = KeyboardExtended.GetState();

            HandleKeyboardState(keyboardState);
        }

        #region private

        private void HandleKeyboardState(KeyboardStateExtended keyboardState)
        {
            HandlePressedKeys(keyboardState);

            HandleReleasedKeys(keyboardState);
        }

        private void HandlePressedKeys(KeyboardStateExtended keyboardState)
        {
            foreach (Keys k in KeyboardHelper.TriggerableKeys)
            {
                if(keyboardState.WasKeyPressed(k))
                {
                    _keyboardService.SendKeyEvent(k, true);
                }
            }
        }

        private void HandleReleasedKeys(KeyboardStateExtended keyboardState)
        {
            foreach (Keys k in KeyboardHelper.TriggerableKeys)
            {
                if (keyboardState.WasKeyReleased(k))
                {
                    _keyboardService.SendKeyEvent(k, false);
                }
            }
        }

        #endregion
    }
}
