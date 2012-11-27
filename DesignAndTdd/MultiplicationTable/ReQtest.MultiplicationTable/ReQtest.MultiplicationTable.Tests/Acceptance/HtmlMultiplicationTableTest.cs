using NUnit.Framework;
using ReQtest.MultiplicationTable.Application;
using ReQtest.MultiplicationTable.Tests.Acceptance.TestsFiles;

namespace ReQtest.MultiplicationTable.Tests.Acceptance
{
    [TestFixture]
    public class HtmlMultiplicationTableTest
    {

        [Test]
        public void Create_a_5_per_15_html_multiplication_table()
        {
            Helper helper = new Helper();
            Program controller = new Program();
            const string expectedFilename = "expected_multiply_17_13.html";
            const string actualFilename = "multiply_17_13.html";
            helper.DeleteFileIfExixts(actualFilename);

            controller.Execute(new string[] { "17", "13", "html" });

            using (helper.ExtractResource(expectedFilename))
            {
                Assert.IsTrue(helper.FilesAreEqual(expectedFilename, actualFilename));
            }
        }
    }
}
