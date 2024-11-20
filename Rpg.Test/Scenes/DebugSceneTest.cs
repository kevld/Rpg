using Microsoft.Xna.Framework;
using Rpg.Core.Scenes;
using Rpg.Core.Services.Interfaces;
using Rpg.Scenes;
using Rpg.Services;
using Rpg.Test.Mocks;

namespace Rpg.Test.Scenes
{
    [TestClass]
    public class DebugSceneTest
    {
        private BaseScene? _scene;
        private GameMock? _gameMock;
        private GameTime? _gameTime;
        private EntityService? _entityService;
#if DEBUG
        [TestInitialize()]
        public void Startup()
        {
            _gameMock = new GameMock();
            _gameMock.InitializeOnly();
            _gameTime = new GameTime();

            IContentService contentServiceMock = new ContentServiceMock(_gameMock);
            IGraphicsService graphicsServiceMock = new GraphicsServiceMock(_gameMock);
            _entityService = new EntityService();

            GameServiceContainer gameServiceContainer = new();
            gameServiceContainer.AddService(contentServiceMock);
            gameServiceContainer.AddService<IGraphicsService>(graphicsServiceMock);
            gameServiceContainer.AddService<IEntityService>(_entityService);
            gameServiceContainer.AddService<IKeyboardService>(new KeyboardService());

            gameServiceContainer.AddService<IConfigService>(new ConfigService(null));

            _scene = new DebugScene(gameServiceContainer);
        }

        [TestCleanup()]
        public void Cleanup()
        {
            _entityService?.Dispose();
            _scene?.Dispose();
            _gameMock?.Dispose();
        }

        [TestMethod]
        public void GetSceneName_NameOk()
        {
            Assert.AreEqual("DebugScene", _scene?.Name);
        }


        [TestMethod]
        public void GetSceneBackground_BackgroundColorOk()
        {
            Assert.AreEqual(Color.Black, _scene?.BackgroundColor);
        }

        [TestMethod]
        public void GetSceneLightLevel_LightLevelOk()
        {
            Assert.AreEqual(1.0f, _scene?.LightLevel);
        }

        [TestMethod]
        public void Init_NotThrowingError()
        {
            try
            {
                _scene?.Initialize();
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void LoardMap_MapIsNotNull()
        {
            _scene?.LoadMap();

            Assert.IsNotNull(_scene?.Map);
        }

        [TestMethod]
        public void LoadMap_RenderMapIsNotNull()
        {
            _scene?.LoadMap();

            Assert.IsNotNull(_scene?.MapRenderer);
        }

        [TestMethod]
        public void DisposeMap_MapIsNull()
        {
            _scene?.LoadMap();
            _scene?.Dispose();

            Assert.IsNull(_scene?.Map);
        }

        [TestMethod]
        public void DisposeMap_MapRendererIsNull()
        {
            _scene?.LoadMap();
            _scene?.Dispose();

            Assert.IsNull(_scene?.MapRenderer);
        }

        [TestMethod]
        public void Draw_IsOk()
        {
            try
            {
                _scene?.LoadMap();
                _scene?.Initialize();
                _scene?.Draw(_gameTime);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void Update_IsOk()
        {
            try
            {
                _scene?.LoadMap();
                _scene?.Initialize();
                _scene?.Update(_gameTime);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void Init_CameraAdded()
        {
            _scene?.Initialize();
            Assert.IsNotNull(_scene?.Camera);
            Assert.AreEqual("main", _scene?.Camera.Name);
            Assert.AreEqual(2.5f, _scene?.Camera.Zoom);
            Assert.AreEqual(Color.Black, _scene?.Camera.BackgroundColour);
        }

        [TestMethod]
        public void Init_PlayerLocation()
        {
            _scene?.Initialize();

            Assert.AreEqual(new Vector2(250, 250), _entityService?.LocalPlayer.WorldPosition);
        }
#endif
    }
}
