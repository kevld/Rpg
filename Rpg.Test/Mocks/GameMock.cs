using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rpg.Test.Mocks
{
    public class GameMock : Game
    {
        public SpriteBatch SpriteBatch { get; set; }
        public GraphicsDeviceManager GraphicsDeviceManager { get; set; }
        public GameMock()
        {
            Content.RootDirectory = "Content";
            GraphicsAdapter.UseReferenceDevice = true;
            GraphicsAdapter.UseDriverType = GraphicsAdapter.DriverType.FastSoftware;
            GraphicsDeviceManager = new GraphicsDeviceManager(this);
            
        }

        public void InitializeOnly()
        {
            if (GraphicsDevice == null)
            {
                var graphicsDeviceManager = Services.GetService(typeof(IGraphicsDeviceManager)) as IGraphicsDeviceManager;
                graphicsDeviceManager?.CreateDevice();
            }
            Initialize();
        }
    }
}
