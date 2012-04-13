using System;
using System.Collections.Generic;

using Moq;
using NUnit.Framework;

using Bookshelf.DataStorage;
using Bookshelf.DomainModel;
using Bookshelf.ApplicationServices;
using RegisteredUser = Bookshelf.DomainModel.RegisteredUser;

namespace Bookshelf.Tests.Unit
{
	[TestFixture]
	public class AdministrativeBooksLoanService_ReturnLoanedBookTest
	{
		private AdministrativeBooksLoanService administrativeBooksLoanService;
		private Mock<IBooksAndRegisteredUsersStorage> stubBooksAndRegisteredUsersStorage;
		private Mock<IBooksStorage> stubBooksStorage;
		private string fromRegisteredUser;

		const int AnyBookIsbn = 1;
		
		[SetUp]
		public void SetUp()
		{

			stubBooksAndRegisteredUsersStorage = new Mock<IBooksAndRegisteredUsersStorage>(MockBehavior.Loose);
			stubBooksStorage = new Mock<IBooksStorage>(MockBehavior.Loose);

			administrativeBooksLoanService = new AdministrativeBooksLoanService(
				null,
				stubBooksAndRegisteredUsersStorage.Object,
				stubBooksStorage.Object,
				null);

		}

		[Test]
		public void ReturnLoanedBook_when_succeed_return_the_registeredUserName_that_had_the_book_on_loan()
		{
			var registeredUserPaoloRossi = new RegisteredUser(11, "Paolo", "Rossi");
			stubBooksAndRegisteredUsersStorage
			.Setup(
				booksAndUsersStorage => booksAndUsersStorage.RetrieveTheRegisteredUserThatHaveTheLoanOfTheBook(It.IsAny<long>())
			)
			.Returns(registeredUserPaoloRossi);

			administrativeBooksLoanService.ReturnLoanedBook(AnyBookIsbn, out fromRegisteredUser);
			Assert.AreEqual("Paolo Rossi", fromRegisteredUser);
		}

		[Test]
		public void ReturnLoanedBook_when_the_book_is_not_loaned_then_raise_an_exception()
		{
			stubBooksAndRegisteredUsersStorage
			.Setup(
				booksAndUsersStorage => booksAndUsersStorage.RetrieveTheRegisteredUserThatHaveTheLoanOfTheBook(It.IsAny<long>())
			)
			.Throws<DataNotFoundException>();

			Assert.Throws<InvalidOperationException>(() =>
				administrativeBooksLoanService.ReturnLoanedBook(AnyBookIsbn, out fromRegisteredUser)
			);
		}

		[Test]
		public void ReturnLoanedBook_when_the_book_is_not_existing_then_raise_an_exception()
		{
			var anyRegisteredUser = new RegisteredUser(AnyBookIsbn, null, null);
			stubBooksAndRegisteredUsersStorage
			.Setup(
				booksAndUsersStorage => booksAndUsersStorage.RetrieveTheRegisteredUserThatHaveTheLoanOfTheBook(It.IsAny<long>())
			)
			.Returns(anyRegisteredUser);

			stubBooksStorage
			.Setup(
				booksStorage => booksStorage.ReturnBook(It.IsAny<long>(), It.IsAny<int>())
			)
			.Throws<DataNotFoundException>();

			Assert.Throws<InvalidOperationException>(() =>
				administrativeBooksLoanService.ReturnLoanedBook(AnyBookIsbn, out fromRegisteredUser)
			);
		}	
	}
}
