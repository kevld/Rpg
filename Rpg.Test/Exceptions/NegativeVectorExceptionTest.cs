using Rpg.Exceptions;

namespace Rpg.Test.Exceptions
{
    [TestClass]
    public class NegativeVectorExceptionTest
    {
        [TestMethod]
        public void ThrowEmptyException()
        {
            Assert.ThrowsException<NegativeVectorException>(() => throw new NegativeVectorException());
        }

        [TestMethod]
        public void ThrowExceptionWithMessage()
        {
            NegativeVectorException ex = Assert.ThrowsException<NegativeVectorException>(() => throw new NegativeVectorException("Expected message text."));
            Assert.AreEqual("Expected message text.", ex.Message);
        }
    }
}
