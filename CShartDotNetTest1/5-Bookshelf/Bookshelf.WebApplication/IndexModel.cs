using System.Collections.Generic;
using Bookshelf.ApplicationServices;

namespace Bookshelf.WebApplication
{
	public class IndexModel
	{
		private IList<BookLoanStatus> bookshelf;
		private IList<RegisteredUser> registeredUsers;

		public string Message { get; set; }
		public string ErrorMessage { get; set; }
		public IList<BookLoanStatus> GetBookshelf()
		{
			return bookshelf;
		}

		public  void SetBookshelf(IList<BookLoanStatus> value)
		{
			bookshelf = value;
		}

		public void SetRegisteredUsers(IList<RegisteredUser> value)
		{
			registeredUsers = value;
		}

		public IList<RegisteredUser> GetRegisteredUsers()
		{
			return registeredUsers;
		}

	}
}