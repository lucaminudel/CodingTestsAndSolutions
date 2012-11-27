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
        Helper _helper;
        Program _controller;
        TextWriter _redirectToFile = null;
        TextWriter _consoleOut;

        [SetUp]
        public void SetUp()
        {
            _helper = new Helper();
            _controller = new Program();
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
            const string actualFilename = "multiply_6_14.txt";
            _helper.DeleteFileIfExixts(actualFilename);

            _redirectToFile = new StreamWriter(actualFilename);
            Console.SetOut(_redirectToFile);

            _controller.Execute(new string[] { });

            _redirectToFile.Close();
            Assert.IsTrue(_helper.FileContains(actualFilename, "Error"));
            Assert.IsTrue(_helper.FileContains(actualFilename, "rows [cols] [output-format]"));
        }

        [Test]
        public void Raise_an_exception_with_a_non_integer_rows_argument()
        {
            const string actualFilename = "multiply_error.txt";
            _helper.DeleteFileIfExixts(actualFilename);

            _redirectToFile = new StreamWriter(actualFilename);
            Console.SetOut(_redirectToFile);

            _controller.Execute(new string[] { "10,123", "20", "Console" });

            _redirectToFile.Close();
            Assert.IsTrue(_helper.FileContains(actualFilename, "Error"));
            Assert.IsTrue(_helper.FileContains(actualFilename, "rows [cols] [output-format]"));

        }

        [Test]
        public void Raise_an_exception_with_a_rows_smaller_then_1()
        {
            const string actualFilename = "multiply_error.txt";
            _helper.DeleteFileIfExixts(actualFilename);

            _redirectToFile = new StreamWriter(actualFilename);
            Console.SetOut(_redirectToFile);

            _controller.Execute(new string[] { "0", "20", "Console" });

            _redirectToFile.Close();
            Assert.IsTrue(_helper.FileContains(actualFilename, "Error"));
            Assert.IsTrue(_helper.FileContains(actualFilename, "rows [cols] [output-format]"));
        }


        [Test]
        public void Raise_an_exception_with_a_non_valid_string_format_argument()
        {
            const string actualFilename = "multiply_error.txt";
            _helper.DeleteFileIfExixts(actualFilename);

            _redirectToFile = new StreamWriter(actualFilename);
            Console.SetOut(_redirectToFile);

            _controller.Execute(new string[] { "10", "20", "pdf" });

            _redirectToFile.Close();
            Assert.IsTrue(_helper.FileContains(actualFilename, "Error"));
            Assert.IsTrue(_helper.FileContains(actualFilename, "rows [cols] [output-format]"));
        }
    }
}
