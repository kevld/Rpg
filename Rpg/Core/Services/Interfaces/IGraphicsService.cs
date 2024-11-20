using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rpg.Core.Services.Interfaces
{
    public interface IGraphicsService : IBaseService
    {
        public GraphicsDeviceManager Graphics { get; set; }
        public GraphicsDevice GraphicsDevice { get; set; }

        public SpriteBatch SpriteBatch { get; set; }

        public RenderTarget2D SceneRenderTarget { get; set; }

        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        public GameWindow Window { get; set; }


    }
}
