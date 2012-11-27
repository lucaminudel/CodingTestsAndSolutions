using System;
using System.Data;
using System.Data.SqlClient;

using Bookshelf.DomainModel;

namespace Bookshelf.DataStorage
{
	public class BooksAndRegisteredUsersStorage: IBooksAndRegisteredUsersStorage
	{
		private readonly string connectionString;
		private readonly IRegisteredUserFactory registeredUserFactory;

		public BooksAndRegisteredUsersStorage(string connectionString, IRegisteredUserFactory registeredUserFactory)
		{
			this.connectionString = connectionString;
			this.registeredUserFactory = registeredUserFactory;
		}

		public IRegisteredUser RetrieveTheRegisteredUserThatHaveTheLoanOfTheBook(long isbn)
		{
			const string selectCommand = 
				"SELECT RU.* FROM  Book B, RegisteredUser RU WHERE B.Loaned_to_RegisteredUser_Id = RU.id AND B.ISBN = @ISBN";

			var sqlConnection = new SqlConnection(connectionString);
			var readCommand = new SqlCommand(selectCommand, sqlConnection);
			readCommand.Parameters.Add("@ISBN", SqlDbType.Decimal);
			readCommand.Parameters["@ISBN"].Value = isbn;

			SqlDataReader reader = null;
			try
			{
				sqlConnection.Open();
				reader = readCommand.ExecuteReader();
				reader.Read();
				var registeredUser = registeredUserFactory.Create(
					reader.GetInt32(reader.GetOrdinal("Id")),
					reader.GetString(reader.GetOrdinal("FirstName")),
					reader.GetString(reader.GetOrdinal("LastName")));

				return registeredUser;
			}
			catch (InvalidOperationException e)
			{
				throw new DataNotFoundException("No book was found with the requested ISBN and booked by some user.", e);
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
		}
	}
}