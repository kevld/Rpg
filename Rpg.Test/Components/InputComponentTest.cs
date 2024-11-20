using Microsoft.Xna.Framework.Input;
using Rpg.Components;
using Rpg.Core.Components;
using Rpg.Core.Services.Interfaces;
using Rpg.Models;
using Rpg.Services;

namespace Rpg.Test.Components
{
    [TestClass]
    public class InputComponentTest
    {
        private Entity? _entity;
        private BaseComponent? _inputComponent;
        private IEntityService? _entityService;

        private KeyboardService? _keyboardService;

        [TestInitialize]
        public void Startup()
        {
            _keyboardService = new KeyboardService();
            _entityService = new EntityService();

            _entity = new();
            _inputComponent = new InputComponent(_entity, _keyboardService, _entityService);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _keyboardService?.Dispose();
            _entityService = null;
            (_inputComponent as InputComponent)?.Dispose();
            _entity = null;
        }

        [TestMethod]
        public void OwnerIsNotNull()
        {
            Assert.IsNotNull(_inputComponent);
            Assert.IsNotNull(_inputComponent.Owner);
        }

        [TestMethod]
        public void Get_NoPressedKeys()
        {
            var inputComponent = _inputComponent as InputComponent;
            Assert.IsNotNull(inputComponent?.GetPressedKeys());
            Assert.AreEqual(0, inputComponent.GetPressedKeys().Count);
        }

        [TestMethod]
        public void KeyboardBinded_KeyPressed()
        {
            var inputComponent = _inputComponent as InputComponent;
            inputComponent?.Initialize();

            _keyboardService?.SendKeyEvent(Keys.Z, true);
            Assert.IsTrue(inputComponent?.GetPressedKeys().Contains(Keys.Z));
        }

        [TestMethod]
        public void KeyboardBinded_KeyReleased()
        {
            var inputComponent = _inputComponent as InputComponent;
            inputComponent?.Initialize();

            _keyboardService?.SendKeyEvent(Keys.Z, false);
            Assert.IsFalse(inputComponent?.GetPressedKeys().Contains(Keys.Z));
        }

        [TestMethod]
        public void KeyboardBinded_KeyPressedAndReleased()
        {
            var inputComponent = _inputComponent as InputComponent;
            inputComponent?.Initialize();

            _keyboardService?.SendKeyEvent(Keys.Z, true);
            Assert.IsTrue(inputComponent?.GetPressedKeys().Contains(Keys.Z));

            _keyboardService?.SendKeyEvent(Keys.Z, false);
            Assert.IsFalse(inputComponent?.GetPressedKeys().Contains(Keys.Z));
        }

        [TestMethod]
        public void KeyboardBinded_TwoKeyPressed()
        {
            var inputComponent = _inputComponent as InputComponent;
            inputComponent?.Initialize();

            _keyboardService?.SendKeyEvent(Keys.Z, true);
            Assert.IsTrue(inputComponent?.GetPressedKeys().Contains(Keys.Z));
            _keyboardService?.SendKeyEvent(Keys.Q, true);
            Assert.IsTrue(inputComponent?.GetPressedKeys().Contains(Keys.Q));
        }

        [TestMethod]
        public void KeyboardBinded_TwoKeyReleased()
        {
            var inputComponent = _inputComponent as InputComponent;
            inputComponent?.Initialize();

            _keyboardService?.SendKeyEvent(Keys.Z, false);
            Assert.IsFalse(inputComponent?.GetPressedKeys().Contains(Keys.Z));
            _keyboardService?.SendKeyEvent(Keys.Q, false);
            Assert.IsFalse(inputComponent?.GetPressedKeys().Contains(Keys.Q));
        }

        [TestMethod]
        public void KeyboardBinded_TwoKeysPressedAndReleased()
        {
            var inputComponent = _inputComponent as InputComponent;
            inputComponent?.Initialize();

            _keyboardService?.SendKeyEvent(Keys.Z, true);
            Assert.IsTrue(inputComponent?.GetPressedKeys().Contains(Keys.Z));
            _keyboardService?.SendKeyEvent(Keys.Q, true);
            Assert.IsTrue(inputComponent?.GetPressedKeys().Contains(Keys.Q));

            _keyboardService?.SendKeyEvent(Keys.Z, false);
            Assert.IsFalse(inputComponent?.GetPressedKeys().Contains(Keys.Z));
            Assert.IsTrue(inputComponent?.GetPressedKeys().Contains(Keys.Q));

            _keyboardService?.SendKeyEvent(Keys.Q, false);
            Assert.IsFalse(inputComponent?.GetPressedKeys().Contains(Keys.Z));
            Assert.IsFalse(inputComponent?.GetPressedKeys().Contains(Keys.Q));

        }
    }
}
