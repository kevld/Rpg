using Microsoft.Xna.Framework;
using Rpg.Managers;
using Rpg.Test.Mocks;

namespace Rpg.Test.Managers
{
    [TestClass]
    public class EntityManagerTest
    {
        private EntityManager? _entityManager;
        private GameMock? _gameMock;
        private GameTime? _gameTime;

        [TestInitialize]
        public void Startup()
        {
            _gameMock = new GameMock();
            _gameMock.InitializeOnly();
            _gameTime = new GameTime();

            GameServiceContainer gameServiceContainer = new();

            _entityManager = new(gameServiceContainer);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _entityManager?.Dispose();
            _gameMock?.Dispose();
            _gameTime = null;
        }

        [TestMethod]
        public void CreateAndGetEntity()
        {
            var e = _entityManager?.CreateEntity();
            Assert.IsNotNull(e);

            Assert.IsTrue(_entityManager?.EntityList.Contains(e));
            Assert.AreEqual(1, _entityManager?.EntityList.Count);
        }
    }
}
