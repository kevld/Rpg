using Microsoft.Xna.Framework;

namespace Rpg.Core.Extensions
{
    public static class Vector2Extension
    {
        public static int GetXY_AddedAsInt(this Vector2 t)
        {
            return (int)(t.X + t.Y);
        }
    }
}
