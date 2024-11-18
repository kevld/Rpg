using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Rpg.Helpers
{
    public static class KeyboardHelper
    {
        public static readonly List<Keys> TriggerableKeys = new List<Keys>()
        {
            Keys.Z, Keys.Q, Keys.S, Keys.D,
            Keys.Up, Keys.Down, Keys.Left, Keys.Right,
            Keys.Enter, Keys.Escape, Keys.Space
        };
    }
}
