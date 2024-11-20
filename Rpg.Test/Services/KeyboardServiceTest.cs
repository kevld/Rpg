using Microsoft.Xna.Framework.Input;
using Rpg.Core.Services.Interfaces;
using Rpg.EventsArgsModels;
using Rpg.Services;

namespace Rpg.Test.Services
{
    [TestClass]
    public class KeyboardServiceTest
    {
        private KeyboardService? _keyboardService;

        [TestInitialize]
        public void Startup()
        {
            _keyboardService = new KeyboardService();
        }

        [TestCleanup]
        public void CleanUp()
        {
            _keyboardService?.Dispose();
        }

        [TestMethod]
        public void SendKeyEvent_PressKey()
        {
            KeyboardEventArgs recevedKeyboardEvent = new();

            // Arrange
            Keys keys = Keys.Up;

            if (_keyboardService == null)
                Assert.Fail("Keyboard service is null");

            _keyboardService.KeyboardEvent += (s, e) =>
            {
                Assert.IsTrue(s is IKeyboardService);
                Assert.IsTrue(e is KeyboardEventArgs);
                recevedKeyboardEvent = (KeyboardEventArgs)e;
            };

            // Act
            _keyboardService.SendKeyEvent(keys, true);

            // Assert
            Assert.AreEqual(Keys.Up, recevedKeyboardEvent.Keys);
            Assert.AreEqual(true, recevedKeyboardEvent?.IsPressed);

        }

        [TestMethod]
        public void SendKeyEvent_ReleaseKey()
        {
            KeyboardEventArgs recevedKeyboardEvent = new();

            // Arrange
            Keys keys = Keys.Down;

            if (_keyboardService == null)
                Assert.Fail("Keyboard service is null");

            _keyboardService.KeyboardEvent += (s, e) =>
            {
                Assert.IsTrue(s is IKeyboardService);
                Assert.IsTrue(e is KeyboardEventArgs);
                recevedKeyboardEvent = (KeyboardEventArgs)e;
            };

            // Act
            _keyboardService.SendKeyEvent(keys, false);

            // Assert
            Assert.AreEqual(Keys.Down, recevedKeyboardEvent.Keys);
            Assert.AreEqual(false, recevedKeyboardEvent?.IsPressed);

        }
    }
}
