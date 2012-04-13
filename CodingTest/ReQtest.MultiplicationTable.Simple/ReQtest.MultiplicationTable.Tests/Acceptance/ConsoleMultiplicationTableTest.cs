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
        [Test]
        public void Display_a_6_per_14_multiplication_table()
        {
            Helper helper = new Helper();
            Program controller = new Program();
            const string expectedFilename = "expected_multiply_6_14.txt";
            const string actualFilename = "multiply_6_14.txt";
            helper.DeleteFileIfExixts(actualFilename);

            using (TextWriter redirectToFile = new StreamWriter(actualFilename))
            {
                TextWriter consoleOut = Console.Out;
                Console.SetOut(redirectToFile);
                controller.Execute(new string[] { "6", "14", "console" });
                Console.SetOut(consoleOut);
            }

            using (helper.ExtractResource(expectedFilename))
            {
                Assert.IsTrue(helper.FilesAreEqual(expectedFilename, actualFilename));
            }
        }
    }
}
