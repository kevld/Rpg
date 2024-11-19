using Microsoft.Xna.Framework;
using Rpg.Interfaces;
using Rpg.Scenes;
using Rpg.Services;
using Rpg.Test.Mocks;

namespace Rpg.Test.Scenes
{
    [TestClass]
    public class DebugSceneTest
    {
        private DebugScene? _scene;
        private GameMock? _gameMock;
        private GameTime? _gameTime;
#if DEBUG
        [TestInitialize()]
        public void Startup()
        {
            _gameMock = new GameMock();
            _gameMock.InitializeOnly();
            _gameTime = new GameTime();

            IContentService contentServiceMock = new ContentServiceMock(_gameMock);
            IGraphicsService graphicsServiceMock = new GraphicsServiceMock(_gameMock);
            IEntityService entityService = new EntityService();

            GameServiceContainer gameServiceContainer = new();
            gameServiceContainer.AddService(contentServiceMock);
            gameServiceContainer.AddService(graphicsServiceMock);
            gameServiceContainer.AddService(entityService);
            gameServiceContainer.AddService<IKeyboardService>(new KeyboardService());

            //TODO: chemin du json
            gameServiceContainer.AddService<IConfigService>(new ConfigService(null));

            _scene = new DebugScene(gameServiceContainer);
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
            _scene?.DisposeMap();

            Assert.IsNull(_scene?.Map);
        }

        [TestMethod]
        public void DisposeMap_MapRendererIsNull()
        {
            _scene?.LoadMap();
            _scene?.DisposeMap();

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
        public void DrawOrder_ThrowEx()
        {
            Assert.ThrowsException<NotImplementedException>(() => _scene?.DrawOrder);
        }

        [TestMethod]
        public void Visible_ThrowEx()
        {
            Assert.ThrowsException<NotImplementedException>(() => _scene?.Visible);
        }

        [TestMethod]
        public void Enabled_ThrowEx()
        {
            Assert.ThrowsException<NotImplementedException>(() => _scene?.Enabled);
        }

        [TestMethod]
        public void UpdateOrder_ThrowEx()
        {
            Assert.ThrowsException<NotImplementedException>(() => _scene?.UpdateOrder);
        }
#endif
    }
}
