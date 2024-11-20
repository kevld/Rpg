using Microsoft.Xna.Framework;
using Rpg.Core.Scenes;
using Rpg.Core.Services.Interfaces;
using Rpg.Scenes;
using Rpg.Services;
using Rpg.Test.Mocks;

namespace Rpg.Test.Scenes
{
    [TestClass]
    public class DebugScene2Test
    {
        private BaseScene? _scene;
        private GameMock? _gameMock;
        private EntityService? _entityService;

#if DEBUG
        [TestInitialize()]
        public void Startup()
        {
            _gameMock = new GameMock();
            _gameMock.InitializeOnly();

            ContentServiceMock contentServiceMock = new ContentServiceMock(_gameMock);
            GraphicsServiceMock graphicsServiceMock = new GraphicsServiceMock(_gameMock);
            _entityService = new EntityService();

            GameServiceContainer gameServiceContainer = new();
            gameServiceContainer.AddService<IContentService>(contentServiceMock);
            gameServiceContainer.AddService<IGraphicsService>(graphicsServiceMock);
            gameServiceContainer.AddService<IEntityService>(_entityService);
            gameServiceContainer.AddService<IKeyboardService>(new KeyboardService());

            gameServiceContainer.AddService<IConfigService>(new ConfigService(null));

            _scene = new DebugScene2(gameServiceContainer);
        }

        [TestCleanup()]
        public void Cleanup()
        {
            _scene?.Dispose();
            _gameMock?.Dispose();
        }

        [TestMethod]
        public void GetSceneName_NameOk()
        {
            Assert.AreEqual("DebugScene2", _scene?.Name);
        }

        [TestMethod]
        public void Init_PlayerLocation()
        {
            _scene?.Initialize();

            Assert.AreEqual(new Vector2(500, 500), _entityService?.LocalPlayer.WorldPosition);
        }

#endif
    }
}
