using Bookshelf.DataStorage;
using Bookshelf.DomainModel;
using NUnit.Framework;

namespace Bookshelf.Tests.Integration
{
	[TestFixture]
	public class BooksAndRegisteredUsersStorageTest
	{
		private BooksAndRegisteredUsersStorage booksAndRegisteredUsersStorage;

		[SetUp]
		public void SetUp()
		{
			booksAndRegisteredUsersStorage = new BooksAndRegisteredUsersStorage(
				new TestDatabaseConnectionString(), 
				new RegisteredUserFactory());			
		}

		[Test]
		public void RetrieveTheRegisteredUserThatHaveTheLoanOfTheBook_when_the_book_is_loaned_then_succeed()
		{
			const long isbnOfABookLoanedToSomeUser = 9780195111309;
			booksAndRegisteredUsersStorage.RetrieveTheRegisteredUserThatHaveTheLoanOfTheBook(isbnOfABookLoanedToSomeUser);
		}

		[Test]
		public void RetrieveTheRegisteredUserThatHaveTheLoanOfTheBook_when_the_book_is_not_loaned_then_raise_an_exception()
		{

			const long isbnOfABookNotLoaned = 9780803972308;
			Assert.Throws<DataNotFoundException>(() => 
				booksAndRegisteredUsersStorage.RetrieveTheRegisteredUserThatHaveTheLoanOfTheBook(isbnOfABookNotLoaned)
			);
		}

		[Test]
		public void RetrieveTheRegisteredUserThatHaveTheLoanOfTheBook_when_the_book_is_not_existing_then_raise_an_exception()
		{

			const long isbnOfANonExistingBook = 0;
			Assert.Throws<DataNotFoundException>(() => 
				booksAndRegisteredUsersStorage.RetrieveTheRegisteredUserThatHaveTheLoanOfTheBook(isbnOfANonExistingBook)
			);
		}
	}
}
