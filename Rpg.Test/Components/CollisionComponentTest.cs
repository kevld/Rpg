using Microsoft.Xna.Framework;
using Rpg.Components;
using Rpg.Interfaces;
using Rpg.Models;
using Rpg.Services;
using Rpg.Test.Mocks;

namespace Rpg.Test.Components
{
    [TestClass]
    public class CollisionComponentTest
    {
        private Entity? _owner;
        private Component? _collisionComponent;
        private GameMock? _gameMock;
        private GameTime? _gameTime;
        private IGraphicsService? _graphicsService;

        [TestInitialize]
        public void Startup()
        {
            _gameTime = new GameTime();
            _gameMock = new GameMock();
            _gameMock.InitializeOnly();

            _owner = new();
            _graphicsService = new GraphicsServiceMock(_gameMock);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _collisionComponent = null;
            _gameTime = null;
            _graphicsService?.Dispose();
            _gameMock?.Dispose();
            _owner = null;
        }

        [TestMethod]
        public void Ctor_Default_OwnerNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new CollisionComponent(null, null, default, default));
        }

        [TestMethod]
        public void Ctor2_Default_OwnerNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new CollisionComponent(null, null, 0, 0));
        }

        [TestMethod]
        public void Ctor_SizeAndOffset()
        {
            _owner = new Entity();

            _collisionComponent = new CollisionComponent(_owner, null, new Vector2(10, 10), new Vector2(15, 15));

            Assert.IsNotNull(_collisionComponent);
            Assert.IsNotNull(_collisionComponent.Owner);

            Assert.AreEqual(typeof(CollisionComponent), _collisionComponent.GetType());
            Assert.IsNotNull((CollisionComponent)_collisionComponent);
            Assert.AreEqual(new Vector2(10, 10), ((CollisionComponent)_collisionComponent).Size);
            Assert.AreEqual(new Vector2(15, 15), ((CollisionComponent)_collisionComponent).Offset);
        }

        [TestMethod]
        public void Ctor2_SizeAndOffset()
        {
            _owner = new Entity();

            _collisionComponent = new CollisionComponent(_owner, null, 10, 10, 15, 15);

            Assert.IsNotNull(_collisionComponent);
            Assert.IsNotNull(_collisionComponent.Owner);

            Assert.AreEqual(typeof(CollisionComponent), _collisionComponent.GetType());
            Assert.IsNotNull((CollisionComponent)_collisionComponent);
            Assert.AreEqual(new Vector2(10, 10), ((CollisionComponent)_collisionComponent).Size);
            Assert.AreEqual(new Vector2(15, 15), ((CollisionComponent)_collisionComponent).Offset);
        }

        [TestMethod]
        public void GetBoundingBox_XY()
        {
            _owner = new Entity();
            _collisionComponent = new CollisionComponent(_owner, null, 10, 10, 15, 15);

            var expected = new Rectangle(40, 60, 10, 10);

            Assert.AreEqual(expected, ((CollisionComponent)_collisionComponent).GetBoundingBox(25, 45));
            Assert.AreEqual(expected, ((CollisionComponent)_collisionComponent).Box);
        }

        [TestMethod]
        public void GetBoundingBox_Vector()
        {
            _owner = new Entity();
            _collisionComponent = new CollisionComponent(_owner, null, 10, 10, 15, 15);

            var expected = new Rectangle(40, 60, 10, 10);

            Assert.AreEqual(expected, ((CollisionComponent)_collisionComponent).GetBoundingBox(new Vector2(25, 45)));
            Assert.AreEqual(expected, ((CollisionComponent)_collisionComponent).Box);
        }

        [TestMethod]
        public void GetBroadphaseBox()
        {
            _owner = new Entity();
            _collisionComponent = new CollisionComponent(_owner, null, 10, 10, 15, 15);

            var expected = new Rectangle(0, 0, 50, 90);

            Assert.AreEqual(expected, ((CollisionComponent)_collisionComponent).GetBroadphaseBox(new Vector2(25, 45)));
            Assert.AreEqual(expected, ((CollisionComponent)_collisionComponent).Broadphase);
        }

        [TestMethod]
        public void GetBroadphaseBox_velocityInferiorToZero()
        {
            _owner = new Entity();
            _collisionComponent = new CollisionComponent(_owner, null, 10, 10, 15, 15);

            var expected = new Rectangle(-50, -90, 50, 90);

            Assert.AreEqual(expected, ((CollisionComponent)_collisionComponent).GetBroadphaseBox(new Vector2(-25, -45)));
            Assert.AreEqual(expected, ((CollisionComponent)_collisionComponent).Broadphase);
        }
    }
}
