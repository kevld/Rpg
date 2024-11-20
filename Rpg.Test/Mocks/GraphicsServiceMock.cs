using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rpg.Core.Services;
using Rpg.Core.Services.Interfaces;

namespace Rpg.Test.Mocks
{
    public class GraphicsServiceMock : BaseService, IGraphicsService
    {
        public GraphicsServiceMock(GameMock gameMock)
        {
            GraphicsDevice = gameMock.GraphicsDevice;
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Graphics = gameMock.GraphicsDeviceManager;
            SceneRenderTarget = new RenderTarget2D(
                GraphicsDevice,
                GraphicsDevice.PresentationParameters.BackBufferWidth,
                GraphicsDevice.PresentationParameters.BackBufferHeight,
                false,
                GraphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24, 0, RenderTargetUsage.PreserveContents
            );

            ScreenWidth = Graphics.PreferredBackBufferWidth;
            ScreenHeight = Graphics.PreferredBackBufferHeight;
            Window = gameMock.Window;
        }

        public GraphicsDeviceManager Graphics { get; set; }
        public GraphicsDevice GraphicsDevice { get; set; }
        public SpriteBatch SpriteBatch { get; set; }
        public RenderTarget2D SceneRenderTarget { get; set; }
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        public GameWindow Window { get; set; }
    }
}
