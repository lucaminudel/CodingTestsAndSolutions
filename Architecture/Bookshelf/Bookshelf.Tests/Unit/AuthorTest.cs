using NUnit.Framework;

using Bookshelf.DomainModel;

namespace Bookshelf.Tests.Unit
{
	[TestFixture]
	public class AuthorTest
	{
		[Test]
		public void SendAuthorInfoTo_send_the_expected_info()
		{
			const int authorId = 11;
			const string authorFirstName = "Bruce";
			const string authorLastName = "Sterling";
			var author = new Author(authorId, authorFirstName, authorLastName);
			author.SendAuthorInfoTo((id, firstName, lastName) =>
			{
				Assert.AreEqual(authorId, id);
				Assert.AreEqual(authorFirstName, firstName);
				Assert.AreEqual(authorLastName, lastName);
			});
		}
	}
}
