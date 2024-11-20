using Rpg.Exceptions;

namespace Rpg.Test.Exceptions
{
    [TestClass]
    public class CameraTypeExceptionTest
    {
        [TestMethod]
        public void ThrowEmptyException()
        {
            Assert.ThrowsException<CameraTypeException>(() => throw new CameraTypeException());
        }

        [TestMethod]
        public void ThrowExceptionWithMessage()
        {
            CameraTypeException ex = Assert.ThrowsException<CameraTypeException>(() => throw new CameraTypeException("Expected message text."));
            Assert.AreEqual("Expected message text.", ex.Message);
        }
    }
}
