using System;
using System.IO;
using NUnit.Framework;
using ReQtest.MultiplicationTable.Application;
using ReQtest.MultiplicationTable.Tests.Acceptance.TestsFiles;

namespace ReQtest.MultiplicationTable.Tests.Acceptance
{
    [TestFixture]
    public class ConsoleMultiplicationTableTest
    {
        TextWriter _redirectToFile = null;
        TextWriter _consoleOut;

        [SetUp]
        public void SetUp()
        {
            _redirectToFile = null;
            _consoleOut = Console.Out;
        }

        [TearDown]
        public void TearDown()
        {
            Console.SetOut(_consoleOut);
            if (_redirectToFile != null)
            {
                _redirectToFile.Dispose();
            }
        }

        [Test]
        public void Display_a_6_per_14_multiplication_table()
        {
            Helper helper = new Helper();
            Program controller = new Program();
            const string expectedFilename = "expected_multiply_6_14.txt";
            const string actualFilename = "multiply_6_14.txt";
            helper.DeleteFileIfExixts(actualFilename);

            _redirectToFile = new StreamWriter(actualFilename);

            Console.SetOut(_redirectToFile);
            controller.Execute(new string[] { "6", "14", "console" });

            _redirectToFile.Close();
            using (helper.ExtractResource(expectedFilename))
            {
                Assert.IsTrue(helper.FilesAreEqual(expectedFilename, actualFilename));
            }
        }
    }
}
