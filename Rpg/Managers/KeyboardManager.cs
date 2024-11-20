using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using Rpg.Core.Managers;
using Rpg.Core.Services.Interfaces;
using Rpg.Helpers;
using Rpg.Interfaces;
using System.Linq;

namespace Rpg.Managers
{
    public class KeyboardManager(GameServiceContainer services) : BaseManager(services), IUpdatable
    {
        private readonly IKeyboardService _keyboardService = services.GetService<IKeyboardService>();

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
            foreach (var k in from Keys k in KeyboardHelper.GetTriggerableKeys()
                              where keyboardState.WasKeyPressed(k)
                              select k)
            {
                _keyboardService.SendKeyEvent(k, true);
            }
        }

        private void HandleReleasedKeys(KeyboardStateExtended keyboardState)
        {
            foreach (var k in from Keys k in KeyboardHelper.GetTriggerableKeys()
                              where keyboardState.WasKeyReleased(k)
                              select k)
            {
                _keyboardService.SendKeyEvent(k, false);
            }
        }

        #endregion
    }
}
