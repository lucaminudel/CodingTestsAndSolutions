using NUnit.Framework;

using Bookshelf.DomainModel;

namespace Bookshelf.Tests.Unit
{
	[TestFixture]
	public class BookTest
	{
		[Test]
		public void SendBookInfoTo_return_the_expected_info()
		{
			const long bookIsbn = 9780441374236;
			const string bookTitle = "Islands in the Net";
			const int bookAuthorId = 11;
			const int bookLoanedToUserId = 100;
			var book = new Book(bookIsbn, bookTitle, bookAuthorId, bookLoanedToUserId);

			book.SendBookInfoTo((isbn, title, authorId, loanedToUserId) =>
			{
				Assert.AreEqual(bookIsbn, isbn);
				Assert.AreEqual(bookTitle, title);
				Assert.AreEqual(bookAuthorId, authorId);
				Assert.AreEqual(bookLoanedToUserId, loanedToUserId);

			});
		}
	}
}
