using System;
using System.Globalization;

using Bookshelf.ApplicationServices;

namespace Bookshelf.WebApplication
{
	public class IndexController
	{
		private readonly IAdministrativeBooksLoanService loanService;
		private readonly IndexModel model;

		public IndexController(IndexModel model, IAdministrativeBooksLoanService loanService)
		{
			this.model = model;
			this.loanService = loanService;
		}

		public void ReloadBookshelf()
		{
			model.SetBookshelf(loanService.GetCurrentStatus());
		}

		public void ReloadRegisteredUsers()
		{
			var registeredUsers = loanService.GetRegisteredUsers();
			registeredUsers.Insert(0, new RegisteredUser() {Id = -1, FullName = "<Select User to Loan>"});
			model.SetRegisteredUsers(registeredUsers);
		}

		public void ExecuteBookReturn(string commandName, string commandArgument)
		{
			if (commandName != "Return")
			{
				return;
			}

			var isbn = long.Parse(commandArgument, CultureInfo.InvariantCulture);

			string from;
			try
			{
				loanService.ReturnLoanedBook(isbn, out from);
			}
			catch (InvalidOperationException)
			{
				model.ErrorMessage = "Return failed: because the book has already been returned, "
					                    + "or the book has been removed from the bookshelf.";
				return;
			}

			model.Message = "Return succeed: book returned by " + from + ".";
		}

		public void ExecuteBookLoan(string isbnString, string registeredUserIdString, string registeredUserFullName)
		{
			var isbn = long.Parse(isbnString, CultureInfo.InvariantCulture);
			var registeredUserId = int.Parse(registeredUserIdString, CultureInfo.InvariantCulture);

			try
			{
				loanService.LoanBook(isbn, registeredUserId);
			}
			catch (InvalidOperationException)
			{
				model.ErrorMessage = "Loan failed: because the book has already been loaned to someone, "
										+ "or the book has been removed from the bookshelf.";
				return;
			}

			model.Message = "Loan succeed: book loaned by " + registeredUserFullName + ".";

		}
	}
}