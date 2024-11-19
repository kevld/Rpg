using Microsoft.Xna.Framework;
using Rpg.Managers;
using Rpg.Test.Mocks;

namespace Rpg.Test.Managers
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class EntityManagerTest
    {
        private EntityManager? _entityManager;
        private GameMock? _gameMock;
#if DEBUG
        [TestInitialize]
        public void Startup()
        {
            _gameMock = new GameMock();
            _gameMock.InitializeOnly();

            GameServiceContainer gameServiceContainer = new();

            _entityManager = new(gameServiceContainer);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _entityManager?.Dispose();
            _gameMock?.Dispose();
        }

        [TestMethod]
        public void CreateAndGetEntity()
        {
            var e = _entityManager?.CreateEntity();
            Assert.IsNotNull(e);

            Assert.IsTrue(_entityManager?.EntityList.Contains(e));
            Assert.AreEqual(1, _entityManager?.EntityList.Count);
        }
#endif
    }
}
