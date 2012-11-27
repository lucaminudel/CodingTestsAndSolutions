using Bookshelf.DataStorage;
using NUnit.Framework;

namespace Bookshelf.Tests.Integration
{
	[TestFixture]
	public class BooksStorageTest
	{
		BooksStorage booksStorage;
		const long isbnOfANonExistingBook = 0;
		const int nonExistingRegisteredUserId = 0;

		[SetUp]
		public void SetUp()
		{
			booksStorage = new BooksStorage(new TestDatabaseConnectionString());			
		}

		[Test]
		public void ReturnBook_when_the_book_is_not_existing_then_raise_an_exception()
		{

			Assert.Throws<DataNotFoundException>(() =>
				booksStorage.ReturnBook(isbnOfANonExistingBook, nonExistingRegisteredUserId)
			);
		}

		[Test]
		public void LoanBook_when_the_book_is_not_existing_then_raise_an_exception()
		{
			Assert.Throws<DataNotFoundException>(() =>
				booksStorage.LoanBook(isbnOfANonExistingBook, nonExistingRegisteredUserId)
			);
		}
	}
}
