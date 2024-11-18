using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rpg.Interfaces;
using System;

namespace Rpg.Services
{
    public class GraphicsService : IGraphicsService
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

        ~GraphicsService() => Dispose();

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
