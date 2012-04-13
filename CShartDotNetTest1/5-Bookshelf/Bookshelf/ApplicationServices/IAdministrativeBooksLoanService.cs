using System.Collections.Generic;

namespace Bookshelf.ApplicationServices
{
	public interface IAdministrativeBooksLoanService
	{
		IList<BookLoanStatus> GetCurrentStatus();
		IList<RegisteredUser> GetRegisteredUsers();
		void ReturnLoanedBook(long isbn, out string fromRegisteredUser);
		void LoanBook(long bookISBN, int byRegisteredUserId);
	}
}