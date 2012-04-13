using NUnit.Framework;

namespace ReQtest.MultiplicationTable.Tests
{
    [TestFixture]
    public class MultiplicationTableTest
    {
        
        [Test]
        public void Return_the_multiplication_of_row_and_column()
        {
            ITable target = new Application.MultiplicationTable();

            Assert.AreEqual(1, target[1, 1]);
            Assert.AreEqual(4, target[2, 2]);
            Assert.AreEqual(9, target[3, 3]);
            Assert.AreEqual(400, target[20, 20]);

            Assert.AreEqual(3, target[1, 3]);
            Assert.AreEqual(3, target[3, 1]);

            Assert.AreEqual(10, target[2, 5]);
            Assert.AreEqual(10, target[5, 2]);
        }
    }
}
