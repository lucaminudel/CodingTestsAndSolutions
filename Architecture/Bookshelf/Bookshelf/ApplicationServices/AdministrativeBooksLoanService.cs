using System;
using System.Collections.Generic;

using Bookshelf.DataStorage;
using Bookshelf.DomainModel;

namespace Bookshelf.ApplicationServices
{
	public class AdministrativeBooksLoanService : IAdministrativeBooksLoanService
	{
		private readonly IBooksStorage booksStorage;
		private readonly IBooksAndAuthorsStorage booksAndAuthorsStorage;
		private readonly IRegisteredUsersStorage registeredUsersStorage;
		private readonly IBooksAndRegisteredUsersStorage booksAndRegisteredUsersStorage;

		public AdministrativeBooksLoanService(
			IBooksAndAuthorsStorage booksAndAuthorsStorage, 
			IBooksAndRegisteredUsersStorage booksAndRegisteredUsersStorage, 
			IBooksStorage booksStorage,
			IRegisteredUsersStorage registeredUsersStorage)
		{
			this.booksAndAuthorsStorage = booksAndAuthorsStorage;
			this.booksAndRegisteredUsersStorage = booksAndRegisteredUsersStorage;
			this.booksStorage = booksStorage;
			this.registeredUsersStorage = registeredUsersStorage;
		}

		public IList<BookLoanStatus> GetCurrentStatus()
		{
			var currentStatus = new List<BookLoanStatus>();

			var allBooks = booksAndAuthorsStorage.RetrieveAllBooksWithAuthors();
			foreach (var book in allBooks)
			{
				var bookStatus = new BookLoanStatus();

				book.Item1.SendBookInfoTo((isbn, title, id, userId) =>
				{
					bookStatus.ISBN = isbn;
					bookStatus.Title = title;
					bookStatus.IsLoaned = userId.HasValue;
				});

				book.Item2.SendAuthorInfoTo((id, firstName, lastName) =>
				{
					bookStatus.Authors = firstName + " " + lastName;				                                     		
				});

				currentStatus.Add(bookStatus);
			}

			return currentStatus;
		}

		public IList<RegisteredUser> GetRegisteredUsers()
		{
			var registeredUsers = new List<RegisteredUser>();

			var allUsers = registeredUsersStorage.RetrieveAllUsers();
			foreach (var registeredUser in allUsers)
			{
				var user = new RegisteredUser();
				registeredUser.SendRegisteredUserInfoTo((id, firstName, lastName) =>
				{
				    user.Id = id;
					user.FullName = firstName + " " + lastName;
				});

				registeredUsers.Add(user);
			}

			return registeredUsers;
		}

		public void ReturnLoanedBook(long isbn, out string fromRegisteredUser)
		{
			IRegisteredUser user;

			try
			{
				user = booksAndRegisteredUsersStorage.RetrieveTheRegisteredUserThatHaveTheLoanOfTheBook(isbn);
			}
			catch (DataNotFoundException e)
			{
				throw CreateExceptionForReturnFailure(e);
			}

			var fullUsername = string.Empty;
			var userId = 0;
			user.SendRegisteredUserInfoTo((id, firsName, lastName) =>
			{
				fullUsername = firsName + " " + lastName;
				userId = id;
			});
			fromRegisteredUser = fullUsername;

			try
			{
				booksStorage.ReturnBook(isbn, fromRegisteredUserId:userId);
			}
			catch (DataNotFoundException e)
			{
				throw CreateExceptionForReturnFailure(e);
			}
		}

		public void LoanBook(long bookISBN, int byRegisteredUserId)
		{
			try
			{
				booksStorage.LoanBook(bookISBN, byRegisteredUserId);
			}
			catch (DataNotFoundException e)
			{
				throw CreateExceptionForLoanFailure(e);
			}
		}

		private static InvalidOperationException CreateExceptionForReturnFailure(DataNotFoundException e)
		{
			const string message = "No book found that is loaned with that ISBN. "
			                       + "Check that a book with that ISBN exists "
			                       + "and that the book is still loaned.";

			return new InvalidOperationException(message, e);
		}

		private static InvalidOperationException CreateExceptionForLoanFailure(DataNotFoundException e)
		{
			const string message = "No book found that is available with that ISBN. "
								   + "Check that a book with that ISBN exists "
								   + "and that the book is still not loaned to someone.";

			return new InvalidOperationException(message, e);
		}
	}
}
