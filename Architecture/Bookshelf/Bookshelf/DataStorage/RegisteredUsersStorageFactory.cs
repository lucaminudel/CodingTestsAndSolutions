
using System;
using Bookshelf.DomainModel;

namespace Bookshelf.DataStorage
{
	public class RegisteredUsersStorageFactory
	{
		private readonly string connectionString;

		public RegisteredUsersStorageFactory(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public IRegisteredUsersStorage Create()
		{
			return new RegisteredUsersStorage(connectionString, new RegisteredUserFactory());
		}
	}
}
