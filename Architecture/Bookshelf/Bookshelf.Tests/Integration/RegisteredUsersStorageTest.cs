using Bookshelf.DataStorage;
using Bookshelf.DomainModel;
using NUnit.Framework;

namespace Bookshelf.Tests.Integration
{
	[TestFixture]
	public class RegisteredUsersStorageTest
	{
		[Test]
		public void RetrieveAllUsers_execute_without_exceptions()
		{
			var registeredUsersStorage = new RegisteredUsersStorage(new TestDatabaseConnectionString(), new RegisteredUserFactory());

			registeredUsersStorage.RetrieveAllUsers();
		}
	}
}
