using Microsoft.Xna.Framework;
using Rpg.Core.Services.Interfaces;
using Rpg.Managers;
using Rpg.Scenes;
using Rpg.Services;
using Rpg.Test.Mocks;
namespace Rpg.Test.Managers
{
    [TestClass]
    public class SceneManagerTest
    {
        private SceneManager? _sceneManager;
        private GameMock? _gameMock;
        private GameTime? _gameTime;
#if DEBUG
        [TestInitialize()]
        public void Startup()
        {
            _gameMock = new GameMock();
            _gameMock.InitializeOnly();
            _gameTime = new GameTime();

            GraphicsServiceMock graphicsService = new(_gameMock);
            ContentServiceMock contentService = new(_gameMock);
            EntityService entityService = new();

            GameServiceContainer gameServiceContainer = new();
            gameServiceContainer.AddService<IGraphicsService>(graphicsService);
            gameServiceContainer.AddService<IContentService>(contentService);
            gameServiceContainer.AddService<IEntityService>(entityService);
            gameServiceContainer.AddService<IKeyboardService>(new KeyboardService());

            _sceneManager = new SceneManager(gameServiceContainer);
        }

        [TestCleanup()]
        public void Cleanup()
        {
            _sceneManager?.Dispose();
            _gameMock?.Dispose();
            _gameTime = null;
        }

        [TestMethod]
        public void SceneManager_Is_Not_Null()
        {
            Assert.IsNotNull(_sceneManager);
        }

        [TestMethod]
        public void InitializeOk()
        {
            try
            {
                _sceneManager?.Initialize();
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void Update()
        {
            try
            {
                _sceneManager?.Update(_gameTime);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void Draw()
        {
            try
            {
                _sceneManager?.Update(_gameTime);
                _sceneManager?.Draw(_gameTime);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void ChangeScene_ActiveSceneIsNotNull()
        {
            _sceneManager?.ChangeScene<DebugScene>();

            Assert.IsNotNull(_sceneManager?.ActiveScene);
        }

        [TestMethod]
        public void Ctor_NoServices_ThrowsException()
        {
            var ex = Assert.ThrowsException<NullReferenceException>(() => new SceneManager(null));
            Assert.AreEqual("Object reference not set to an instance of an object.", ex.Message);
        }

#endif
    }
}
