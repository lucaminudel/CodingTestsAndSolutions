using System.Data;
using System.Data.SqlClient;

namespace Bookshelf.DataStorage
{
	public class BooksStorage : IBooksStorage
	{
		private readonly string connectionString;

		public BooksStorage(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public void ReturnBook(long isbn, int fromRegisteredUserId)
		{
			const string updateCommand = 
				"UPDATE Book SET Loaned_to_RegisteredUser_Id = NULL " 
				+ "WHERE Loaned_to_RegisteredUser_Id = @UserId AND ISBN = @ISBN";

			using(var connection = new SqlConnection(connectionString))
			using(var returnCommand = new SqlCommand(updateCommand, connection))
			{
				returnCommand.Parameters.Add("@UserId", SqlDbType.Int);
				returnCommand.Parameters["@UserId"].Value = fromRegisteredUserId;
				returnCommand.Parameters.Add("@ISBN", SqlDbType.Decimal);
				returnCommand.Parameters["@ISBN"].Value = isbn;

				connection.Open();
				var rowsAffected = returnCommand.ExecuteNonQuery();

				if (rowsAffected == 0)
				{
					throw new DataNotFoundException("No book was found with the requested ISBN and booked by some user.");
				}
			}
		}

		public void LoanBook(long bookIsbn, int byRegisteredUserId)
		{
			const string updateCommand =
				"UPDATE Book SET Loaned_to_RegisteredUser_Id = @UserId "
				+ "WHERE ISBN = @ISBN AND Loaned_to_RegisteredUser_Id IS NULL ";

			using (var connection = new SqlConnection(connectionString))
			using (var returnCommand = new SqlCommand(updateCommand, connection))
			{
				returnCommand.Parameters.Add("@UserId", SqlDbType.Int);
				returnCommand.Parameters["@UserId"].Value = byRegisteredUserId;
				returnCommand.Parameters.Add("@ISBN", SqlDbType.Decimal);
				returnCommand.Parameters["@ISBN"].Value = bookIsbn;

				connection.Open();
				var rowsAffected = returnCommand.ExecuteNonQuery();

				if (rowsAffected == 0)
				{
					throw new DataNotFoundException("No book was found with the requested ISBN and available to be loaned.");
				}
			}
		}
	}
}