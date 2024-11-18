using Rpg.Models.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpg.Test.Models.Config
{
    [TestClass]
    public class SettingsTest
    {
        [TestMethod]
        public void IsNotNull()
        {
            Settings s = new();
            Assert.IsNotNull(s);
        }

        [TestMethod]
        public void HasSettings()
        {
            Settings s = new()
            {
                Globals = new()
                {
                    EnableDebug = true
                }
            };
            
            Assert.IsNotNull(s);
            Assert.IsTrue(s.Globals.EnableDebug);
        }
    }
}
