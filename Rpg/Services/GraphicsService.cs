using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rpg.Core.Services;
using Rpg.Core.Services.Interfaces;

namespace Rpg.Services
{
    public class GraphicsService : BaseService, IGraphicsService
    {
        public GraphicsDeviceManager Graphics { get; set; }
        public GraphicsDevice GraphicsDevice { get; set; }

        public SpriteBatch SpriteBatch { get; set; }

        public RenderTarget2D SceneRenderTarget { get; set; }
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        public GameWindow Window { get; set; }

        public GraphicsService(GraphicsDeviceManager graphics)
        {
            Graphics = graphics;
            Graphics.PreferredBackBufferWidth = 800;
            Graphics.PreferredBackBufferHeight = 600;
            Graphics.ApplyChanges();

            ScreenWidth = Graphics.PreferredBackBufferWidth;
            ScreenHeight = Graphics.PreferredBackBufferHeight;
        }
    }
}
