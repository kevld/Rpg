using Microsoft.Xna.Framework;
using Rpg.Services;

namespace Rpg.Test.Services
{
    [TestClass]
    public class EntityServiceTest
    {
        private EntityService? _entityService;

        [TestInitialize]
        public void Startup()
        {
            _entityService = new();
        }

        [TestCleanup]
        public void CleanUp()
        {
            _entityService?.Dispose();
        }

        [TestMethod]
        public void EntityService_IsNotNull()
        {
            Assert.IsNotNull(_entityService);
        }

        [TestMethod]
        public void Entities_NotEmpty()
        {
            Assert.IsNotNull(_entityService?.GetEntities());
        }

        [TestMethod]
        public void LocalPlayer_Null()
        {
            Assert.IsNull(_entityService?.LocalPlayer);
        }

        [TestMethod]
        public void CreateAndGet1Entity()
        {
            _entityService?.CreateEntity();

            Assert.AreEqual(1, _entityService?.GetEntities().Count);
        }

        [TestMethod]
        public void CreateAndGet2Entities()
        {
            _entityService?.CreateEntity();
            _entityService?.CreateEntity(0, 0, 0, 0);

            Assert.AreEqual(2, _entityService?.GetEntities().Count);
        }

        [TestMethod]
        public void CreateAndGet3Entities()
        {
            _entityService?.CreateEntity();
            _entityService?.CreateEntity(0, 0, 0, 0);
            _entityService?.CreatePlayerEntity(1, 1, 1, 1);

            Assert.AreEqual(3, _entityService?.GetEntities().Count);
            Assert.IsNotNull(_entityService?.LocalPlayer);
            Assert.AreEqual(new Vector2(1, 1), _entityService?.LocalPlayer.WorldPosition);
        }

        [TestMethod]
        public void ClearEntities()
        {
            _entityService?.CreateEntity();
            _entityService?.CreateEntity(0, 0, 0, 0);
            _entityService?.CreatePlayerEntity(1, 1, 1, 1);

            Assert.AreEqual(3, _entityService?.GetEntities().Count);
            Assert.IsNotNull(_entityService?.LocalPlayer);
            Assert.AreEqual(new Vector2(1, 1), _entityService?.LocalPlayer.WorldPosition);

            _entityService?.ClearEntities();
            Assert.IsNull(_entityService?.LocalPlayer);
            Assert.AreEqual(0, _entityService?.GetEntities().Count);
        }
    }
}
