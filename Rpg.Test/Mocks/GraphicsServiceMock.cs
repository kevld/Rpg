using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rpg.Interfaces;
namespace Rpg.Test.Mocks
{
    public class GraphicsServiceMock : IGraphicsService
    {
        private bool _disposed;

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

        ~GraphicsServiceMock() => Dispose(false);

        public GraphicsDeviceManager Graphics { get; set; }
        public GraphicsDevice GraphicsDevice { get; set; }
        public SpriteBatch SpriteBatch { get; set; }
        public RenderTarget2D SceneRenderTarget { get; set; }
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        public GameWindow Window { get; set; }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // TODO: dispose managed state (managed objects).
            }

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.

            _disposed = true;
        }
    }
}
