using Rpg.Exceptions;

namespace Rpg.Test.Exceptions
{
    [TestClass]
    public class NegativeNumberExceptionTest
    {
        [TestMethod]
        public void ThrowEmptyException()
        {
            Assert.ThrowsException<NegativeNumberException>(() => throw new NegativeNumberException());
        }

        [TestMethod]
        public void ThrowExceptionWithMessage()
        {
            NegativeNumberException ex = Assert.ThrowsException<NegativeNumberException>(() => throw new NegativeNumberException("Expected message text."));
            Assert.AreEqual("Expected message text.", ex.Message);
        }
    }
}
