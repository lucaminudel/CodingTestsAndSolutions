using System;
using System.IO;
using NUnit.Framework;
using ReQtest.MultiplicationTable.Application;
using ReQtest.MultiplicationTable.Tests.Acceptance.TestsFiles;

namespace ReQtest.MultiplicationTable.Tests.Acceptance
{
    [TestFixture]
    public class DefaultAndOptionalArgsTest
    {
        Helper _helper;
        Program _controller;

        [SetUp]
        public void SetUp()
        {
            _helper = new Helper();
            _controller = new Program();
        }

        [Test]
        public void When_the_command_line_has_only_rows_and_columns_then_default_to_the_console_output_format()
        {
            string[] args = new string[] { "10", "20" };
            const string expectedFilename = "multiply_10_20.txt";
            _helper.DeleteFileIfExixts(expectedFilename);

            using (TextWriter redirectToFile = new StreamWriter(expectedFilename))
            {
                TextWriter consoleOut = Console.Out;
                Console.SetOut(redirectToFile);

                _controller.Execute(args);

                Console.SetOut(consoleOut);
            }

            Assert.IsTrue(_helper.FileExists(expectedFilename));
        }

        [Test]
        public void When_the_command_line_has_only_rows_then_columns_default_to_the_rows_value()
        {
            string[] args = new string[] { "10", "html" };
            const string expectedFilename = "multiply_10_10.html";
            _helper.DeleteFileIfExixts(expectedFilename);

            _controller.Execute(args);

            Assert.IsTrue(_helper.FileExists(expectedFilename));
        }

        [Test]
        public void When_the_command_line_has_only_rows_then_default_to_the_console_output_format_and_columns_default_to_the_rows_value()
        {
            string[] args = new string[] { "10" };
            const string expectedFilename = "multiply_10_10.txt";
            _helper.DeleteFileIfExixts(expectedFilename);

            using (TextWriter redirectToFile = new StreamWriter(expectedFilename))
            {
                TextWriter consoleOut = Console.Out;
                Console.SetOut(redirectToFile);

                _controller.Execute(args);

                Console.SetOut(consoleOut);
            }

            Assert.IsTrue(_helper.FileExists(expectedFilename));
        }


        [Test]
        public void When_all_arguments_are_specified_use_the_specified_arguments()
        {
            string[] args = new string[] { "10", "13", "csv" };
            const string expectedFilename = "multiply_10_13.csv";
            _helper.DeleteFileIfExixts(expectedFilename);

            _controller.Execute(args);

            Assert.IsTrue(_helper.FileExists(expectedFilename));
        }
    }
}
