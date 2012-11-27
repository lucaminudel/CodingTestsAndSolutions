
using Bookshelf.DomainModel;

namespace Bookshelf.DataStorage
{
	public class BooksAndAuthorsStorageFactory
	{
		private readonly string connectionString;

		public BooksAndAuthorsStorageFactory(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public IBooksAndAuthorsStorage Create()
		{
			return new BooksAndAuthorsStorage(connectionString, new BookFactory(), new AuthorFactory());
		}
	}
}
