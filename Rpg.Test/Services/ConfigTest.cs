using Rpg.Services;

namespace Rpg.Test.Services
{
    [TestClass]
    public class ConfigTest
    {
        private readonly string _jsonPath = @"settings.json";

        [TestMethod]
        public void LoadJsonConfigFile_CtorNull()
        {
            Assert.IsNotNull(new ConfigService(null));
        }

        [TestMethod]
        public void LoadJsonConfigFile_CtorEmpty()
        {
            Assert.IsNotNull(new ConfigService(""));
        }

        [TestMethod]
        public void LoadJsonConfigFile_CtorNotFoundFile()
        {
            Assert.IsNotNull(new ConfigService(Guid.NewGuid().ToString()));
        }

        [TestMethod]
        public void GetIsDebug_Default()
        {
            ConfigService configService = new(null);
            Assert.IsFalse(configService.IsDebug);
        }

        [TestMethod]
        public void GetIsDebug_Json()
        {
            ConfigService configService = new(_jsonPath);
            Assert.IsTrue(configService.IsDebug);
        }
    }
}
