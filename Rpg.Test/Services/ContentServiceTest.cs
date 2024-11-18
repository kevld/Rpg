using Rpg.Services;
using Rpg.Test.Mocks;

namespace Rpg.Test.Services
{
    [TestClass]
    public class ContentServiceTest
    {
        private GameMock? _gameMock;

        [TestMethod]
        public void Ctor()
        {
            ContentService service = new();
            Assert.IsNotNull(service);
            Assert.IsNull(service.ContentManager);
        }

        [TestMethod]
        public void ContentNotNull()
        {
            _gameMock = new GameMock();
            _gameMock.InitializeOnly();

            ContentService service = new();
            service.ContentManager = _gameMock.Content;

            Assert.IsNotNull(service.ContentManager);
        }
    }
}
