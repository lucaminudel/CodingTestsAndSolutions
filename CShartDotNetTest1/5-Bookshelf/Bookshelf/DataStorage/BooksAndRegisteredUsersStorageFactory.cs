
using Bookshelf.DomainModel;

namespace Bookshelf.DataStorage
{
	public class BooksAndRegisteredUsersStorageFactory
	{
		private readonly string connectionString;

		public BooksAndRegisteredUsersStorageFactory(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public IBooksAndRegisteredUsersStorage Create()
		{
			return new BooksAndRegisteredUsersStorage(connectionString, new RegisteredUserFactory());
		}			
	}
}
