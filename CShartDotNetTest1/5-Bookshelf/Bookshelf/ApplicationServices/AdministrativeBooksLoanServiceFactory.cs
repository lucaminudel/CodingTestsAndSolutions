using Bookshelf.DataStorage;

namespace Bookshelf.ApplicationServices
{
	public class AdministrativeBooksLoanServiceFactory
	{
		private readonly string connectionString;

		public AdministrativeBooksLoanServiceFactory(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public  IAdministrativeBooksLoanService Create()
		{
			return new AdministrativeBooksLoanService(
				new BooksAndAuthorsStorageFactory(connectionString).Create(),
				new BooksAndRegisteredUsersStorageFactory(connectionString).Create(),
				new BooksStorage(connectionString),
				new RegisteredUsersStorageFactory(connectionString).Create()
			);			
		}
	}
}
