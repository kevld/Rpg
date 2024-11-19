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
            Console.WriteLine("aa");
            GraphicsAdapter.UseReferenceDevice = true;
            GraphicsAdapter.UseDriverType = GraphicsAdapter.DriverType.FastSoftware;
            GraphicsDeviceManager = new GraphicsDeviceManager(this);
            Console.WriteLine("aab");

        }

        public void InitializeOnly()
        {
            if (GraphicsDevice == null)
            {
                Console.WriteLine("aac");

                var graphicsDeviceManager = Services.GetService(typeof(IGraphicsDeviceManager)) as IGraphicsDeviceManager;
            Console.WriteLine("aad");
                graphicsDeviceManager?.CreateDevice();
            Console.WriteLine("aae");
            }
            Initialize();
        }
    }
}
