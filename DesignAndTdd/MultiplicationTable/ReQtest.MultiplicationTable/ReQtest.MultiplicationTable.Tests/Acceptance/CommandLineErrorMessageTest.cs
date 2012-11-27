using System;
using System.IO;
using NUnit.Framework;
using ReQtest.MultiplicationTable.Application;
using ReQtest.MultiplicationTable.Tests.Acceptance.TestsFiles;

namespace ReQtest.MultiplicationTable.Tests.Acceptance
{
    [TestFixture]
    public class CommandLineErrorMessageTest
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
        public void When_no_arguments_are_provided_an_error_message_is_displayed()
        {
            Helper helper = new Helper();
            Program controller = new Program();
            const string actualFilename = "multiply_6_14.txt";
            helper.DeleteFileIfExixts(actualFilename);

            _redirectToFile = new StreamWriter(actualFilename);
            
            Console.SetOut(_redirectToFile);
            controller.Execute(new string[] { });
            
            _redirectToFile.Close();
            Assert.IsTrue(helper.FileContains(actualFilename, "Error"));
            Assert.IsTrue(helper.FileContains(actualFilename, "rows [cols] [output-format]"));
        }
    }
}
