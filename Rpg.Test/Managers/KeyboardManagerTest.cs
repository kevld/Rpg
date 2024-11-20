using Microsoft.Xna.Framework;
using Rpg.Managers;
namespace Rpg.Test.Managers
{
    [TestClass]
    public class KeyboardManagerTest
    {
        private KeyboardManager? _keyboardManager;

        [TestInitialize]
        public void Startup()
        {
            GameServiceContainer gameServiceContainer = new();

            _keyboardManager = new KeyboardManager(gameServiceContainer);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _keyboardManager?.Dispose();
        }

        [TestMethod]
        public void Update_IsOk()
        {
            try
            {
                _keyboardManager?.Update(new GameTime());
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
