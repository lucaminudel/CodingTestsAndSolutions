using System.Data.SqlClient;
using System.Collections.Generic;

using Bookshelf.DomainModel;

namespace Bookshelf.DataStorage
{
	public class RegisteredUsersStorage: IRegisteredUsersStorage
	{
		private readonly string connectionString;
		private readonly IRegisteredUserFactory registeredUserFactory;

		public RegisteredUsersStorage(string connectionString, IRegisteredUserFactory registeredUserFactory)
		{
			this.connectionString = connectionString;
			this.registeredUserFactory = registeredUserFactory;
		}

		public IList<IRegisteredUser> RetrieveAllUsers()
		{
			const string selectCommand = "SELECT * FROM  RegisteredUser";

			var sqlConnection = new SqlConnection(connectionString);
			var readCommand = new SqlCommand(selectCommand, sqlConnection);

			var allRegisteredUsers = new List<IRegisteredUser>();
			SqlDataReader reader = null;
			try
			{
				sqlConnection.Open();
				reader = readCommand.ExecuteReader();
				while (reader.Read())
				{
					var registeredUser = registeredUserFactory.Create(
							reader.GetInt32(reader.GetOrdinal("Id")),
							reader.GetString(reader.GetOrdinal("FirstName")),
							reader.GetString(reader.GetOrdinal("LastName"))
						);

					allRegisteredUsers.Add(registeredUser);
				}
			}
			finally
			{
				if (reader != null)
				{
					reader.Dispose();
				}
				readCommand.Dispose();
				sqlConnection.Dispose();
			}

			return allRegisteredUsers;
		}
	}
}