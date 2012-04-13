using System;
using NUnit.Framework;
using ReQtest.MultiplicationTable.Commands;

namespace ReQtest.MultiplicationTable.Tests
{
    [TestFixture]
    public class CommandLineTest
    {
        private CommandLine target;

        [SetUp]
        public void SetUp()
        {
            target = new CommandLine();
        }

        [Test]
        public void Can_read_a_valid_command_line()
        {
            target.Process(new string[] { "10", "20", "Console" });


            Assert.AreEqual(10, target.Rows);
            Assert.AreEqual(20, target.Columns);
            Assert.AreEqual(OutputFormat.Console, target.OutputFormat);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Raise_an_exception_with_a_non_integer_rows_argument()
        {
            target.Process(new string[] { "10,123", "20", "Console" });
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Raise_an_exception_with_a_rows_smaller_then_1()
        {
            target.Process(new string[] { "0", "20", "Console" });
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Raise_an_exception_with_a_rows_greater_then_20()
        {
            target.Process(new string[] { "21", "20", "Console" }); 
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Raise_an_exception_with_a_non_integer_colums_argument()
        {
            target.Process(new string[] { "10", "20ABC", "Console" });
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Raise_an_exception_with_a_columns_smaller_then_1()
        {
            target.Process(new string[] { "10", "0", "Console" });
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Raise_an_exception_with_a_columns_greater_then_20()
        {
            target.Process(new string[] { "10", "21", "Console" });
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Raise_an_exception_with_a_non_valid_numeric_format_argument()
        {
            target.Process(new string[] { "10", "20", "22" }); 
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Raise_an_exception_with_a_non_valid_string_format_argument()
        {
            target.Process(new string[] { "10", "20", "pdf" });
        }
    }

}
