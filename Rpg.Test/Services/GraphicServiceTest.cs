using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rpg.Interfaces;
using Rpg.Services;
using Rpg.Test.Mocks;

namespace Rpg.Test.Services
{
    [TestClass]
    public class GraphicServiceTest
    {
        private GameMock? _gameMock;
        private IGraphicsService? _graphicsService;

#if DEBUG
        [TestInitialize()]
        public void Startup()
        {
            _gameMock = new GameMock();
            _gameMock.InitializeOnly();

        }

        [TestCleanup()]
        public void Cleanup()
        {
            _graphicsService?.Dispose();
            _gameMock?.Dispose();
        }

        [TestMethod]
        public void GraphicServiceMock_FieldsNotNull()
        {
            if (_gameMock == null)
                Assert.Fail("GameMock is null");

            _graphicsService = new GraphicsServiceMock(_gameMock);
            Assert.IsNotNull(_graphicsService.SpriteBatch);
            Assert.IsNotNull(_graphicsService.SceneRenderTarget);
            Assert.IsNotNull(_graphicsService.GraphicsDevice);
            Assert.IsNotNull(_graphicsService.Graphics);
            Assert.IsTrue(_graphicsService.ScreenWidth > 0);
            Assert.IsNotNull(_graphicsService.ScreenHeight > 0);
        }

        [TestMethod]
        public void GraphicsService_Ctor_DefaultProperties()
        {
            _graphicsService = new GraphicsService(_gameMock?.GraphicsDeviceManager);


            Assert.IsNotNull(_graphicsService.Graphics);
            Assert.IsNull(_graphicsService.GraphicsDevice);
            Assert.IsNull(_graphicsService.SpriteBatch);
            Assert.IsNull(_graphicsService.SceneRenderTarget);
            Assert.IsNull(_graphicsService.Window);
            Assert.AreEqual(800, _graphicsService.ScreenWidth);
            Assert.AreEqual(600, _graphicsService.ScreenHeight);
        }

        [TestMethod]
        public void GraphicsService_Dispose()
        {
            try
            {
                _graphicsService = new GraphicsService(_gameMock?.GraphicsDeviceManager);
                _graphicsService.Dispose();
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }
#endif
    }
}
