namespace Rpg.Test
{
    [TestClass]
    public class Game1Test
    {
        private Game1? _game1;

        [TestInitialize()]
        public void Startup()
        {
            _game1 = new Game1();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            _game1?.Exit();
        }
    }
}