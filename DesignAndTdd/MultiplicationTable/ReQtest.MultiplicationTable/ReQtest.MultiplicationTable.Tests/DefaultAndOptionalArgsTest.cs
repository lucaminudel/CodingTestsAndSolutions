using System;
using NUnit.Framework;
using ReQtest.MultiplicationTable.Commands;

namespace ReQtest.MultiplicationTable.Tests
{
    [TestFixture]
    public class DefaultAndOptionalArgsTest
    {
        const string DefaultOutputFormat = "console";
        DefaultAndOptionalArgs _target;

        [SetUp]
        public void SetUp()
        {
            _target = new DefaultAndOptionalArgs();            
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Raise_an_exception_with_an_empty_command_line()
        {
            _target.ExpandDefaultsAndOptionalArgs(new string[0]);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Raise_an_exception_with_four_command_line_arguments()
        {
            _target.ExpandDefaultsAndOptionalArgs(new string[4]);
        }

        [Test]
        public void Can_read_a_command_line_with_only_rows_and_columns()
        {
            string[] args = _target.ExpandDefaultsAndOptionalArgs(new string[] {"10", "20"});

            Assert.AreEqual("10", args[0]);
            Assert.AreEqual("20", args[1]);
            Assert.AreEqual(DefaultOutputFormat, args[2]);
        }

        [Test]
        public void Can_read_a_command_line_with_only_rows_and_format()
        {
            string[] args = _target.ExpandDefaultsAndOptionalArgs(new string[] { "10", "html" });

            Assert.AreEqual("10", args[0]);
            string columsDefaultToRows = "10";
            Assert.AreEqual(columsDefaultToRows, args[1]);
            Assert.AreEqual("html", args[2]);
        }

        [Test]
        public void Can_read_a_command_line_with_only_rows()
        {
            string[] args = _target.ExpandDefaultsAndOptionalArgs(new string[] { "10" });

            Assert.AreEqual("10", args[0]);
            string columsDefaultToRows = "10";
            Assert.AreEqual(columsDefaultToRows, args[1]);
            Assert.AreEqual(DefaultOutputFormat, args[2]);
        }


        [Test]
        public void When_all_arguments_are_specified_return_the_same_arguments()
        {
            string[] args = _target.ExpandDefaultsAndOptionalArgs(new string[] { "10", "20", "console" });

            Assert.AreEqual("10", args[0]);
            Assert.AreEqual("20", args[1]);
            Assert.AreEqual("console", args[2]);
        }
    }
}
