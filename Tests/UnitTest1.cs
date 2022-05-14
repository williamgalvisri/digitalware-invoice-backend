using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class Tests
    {

        [TestMethod]
        public void Test1()
        {
            int value = 2;

            Assert.AreEqual("1", value);
        }
    }
}