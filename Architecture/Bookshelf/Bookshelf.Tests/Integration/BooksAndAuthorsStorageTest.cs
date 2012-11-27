using Bookshelf.DataStorage;
using Bookshelf.DomainModel;
using NUnit.Framework;

namespace Bookshelf.Tests.Integration
{
	[TestFixture]
	public class BooksAndAuthorsStorageTest
	{
		[Test]
		public void RetrieveAllBooksWithAuthors_SQL_execute_without_errors()
		{
			var booksAndAuthorsSorage = new BooksAndAuthorsStorage(new TestDatabaseConnectionString(), new BookFactory(), new AuthorFactory());

			booksAndAuthorsSorage.RetrieveAllBooksWithAuthors();
		}
	}
}
