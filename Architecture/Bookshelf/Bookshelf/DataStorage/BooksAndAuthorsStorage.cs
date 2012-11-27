using System.Data.SqlClient;
using System.Collections.Generic;

using Bookshelf.DomainModel;
using BookAndAuthorTupla = System.Tuple<Bookshelf.DomainModel.IBook, Bookshelf.DomainModel.IAuthor>;

namespace Bookshelf.DataStorage
{
	public class BooksAndAuthorsStorage : IBooksAndAuthorsStorage
	{
		private readonly string connectionString;
		private readonly IBookFactory bookFactory;
		private readonly IAuthorFactory authorFactory;

		public BooksAndAuthorsStorage(string connectionString, IBookFactory bookFactory, IAuthorFactory authorFactory)
		{
			this.connectionString = connectionString;
			this.bookFactory = bookFactory;
			this.authorFactory = authorFactory;
		}

		public IList<BookAndAuthorTupla> RetrieveAllBooksWithAuthors()
		{
			const string selectCommand = "SELECT * FROM  BOOK B, AUTHOR A WHERE B.Author_Id = A.id ORDER BY B.TITLE";

			var sqlConnection = new SqlConnection(connectionString);
			var readCommand = new SqlCommand(selectCommand, sqlConnection);

			var allBooksWithAuthors = new List<BookAndAuthorTupla>();
			SqlDataReader reader = null;

			try
			{
				sqlConnection.Open();
				reader = readCommand.ExecuteReader();
				while (reader.Read())
				{
					var book = bookFactory.Create(
						(long)reader.GetSqlDecimal(reader.GetOrdinal("ISBN")).Value,
						reader.GetString(reader.GetOrdinal("Title")),
						reader.GetInt32(reader.GetOrdinal("Author_Id")),
						reader.GetSqlInt32(reader.GetOrdinal("Loaned_to_RegisteredUser_Id")).IsNull
							? (int?)null
							: reader.GetInt32(reader.GetOrdinal("Loaned_to_RegisteredUser_Id")));

					var author = authorFactory.Create(
						reader.GetInt32(reader.GetOrdinal("Author_Id")),
						reader.GetString(reader.GetOrdinal("FirstName")),
						reader.GetString(reader.GetOrdinal("LastName")));

					allBooksWithAuthors.Add(new BookAndAuthorTupla(book, author));
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

			return allBooksWithAuthors;
		}
	}
}
