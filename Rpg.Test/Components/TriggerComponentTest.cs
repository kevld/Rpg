using Microsoft.Xna.Framework;
using Rpg.Components;
using Rpg.Models;
using Rpg.Services;

namespace Rpg.Test.Components
{
    [TestClass]
    public class TriggerComponentTest
    {
        private EntityService? _entityService;
        private Entity? _entity;
        private TriggerComponent? _triggerComponent;

        [TestInitialize()]
        public void Startup()
        {
            _entityService = new EntityService();
            _entityService.CreatePlayerEntity(0, 0, 10, 10);

            _entity = _entityService.CreateEntity();
            _triggerComponent = new(_entity, new Rectangle(20, 20, 20, 20));

            Assert.IsTrue(_triggerComponent.AddSpy(_entityService.LocalPlayer));
            _entity.AddComponent(_triggerComponent);
        }

        [TestCleanup()]
        public void Cleanup()
        {
            _triggerComponent?.Dispose();
            _entity = null;
            _entityService?.Dispose();
        }

        [TestMethod]
        public void SpyOnNull()
        {
            TriggerComponent tc = new(_entity, new Rectangle(20, 20, 20, 20));

            Assert.IsFalse(tc.AddSpy(null));
        }

        [TestMethod]
        public void ResetTrigger()
        {
            _triggerComponent?.ResetTrigger(false);
            Assert.IsFalse(_triggerComponent?.HasBeenEnabled);
        }

        [TestMethod]
        public void ResetTrigger_True()
        {
            _triggerComponent?.ResetTrigger(true);
            Assert.IsFalse(_triggerComponent?.HasBeenEnabled);
        }

        [TestMethod]
        public void Update_IsOk()
        {
            try
            {
                _triggerComponent?.Update(new GameTime());
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void Update_IsOk_Trigger()
        {
            try
            {
                if (_entityService == null)
                    Assert.Fail("Entity is null");

                _entityService.LocalPlayer.WorldPosition = new Vector2(25, 25);
                _triggerComponent?.Update(new GameTime());

                Assert.IsTrue(_triggerComponent?.HasBeenEnabled);

                _triggerComponent?.Update(new GameTime());
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
