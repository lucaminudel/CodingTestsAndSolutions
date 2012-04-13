using System;

using Moq;
using NUnit.Framework;

using Bookshelf.DataStorage;
using Bookshelf.ApplicationServices;

namespace Bookshelf.Tests.Unit
{
	[TestFixture]
	public class AdministrativeBooksLoanService_LoanBookTest
	{
		private AdministrativeBooksLoanService administrativeBooksLoanService;
		private Mock<IBooksStorage> stubBooksStorage;

		const int AnyRegisteredUser = 33;
		const int AnyBookIsbn = 1;
		
		[SetUp]
		public void SetUp()
		{
			stubBooksStorage = new Mock<IBooksStorage>(MockBehavior.Loose);

			administrativeBooksLoanService = new AdministrativeBooksLoanService(
				null,
				null,
				stubBooksStorage.Object,
				null);
		}

		[Test]
		public void LoanBook_when_the_persistence_succeed_then_the_whole_operation_succeed_without_exceptions()
		{
			administrativeBooksLoanService.LoanBook(AnyBookIsbn, AnyRegisteredUser);
		}

		[Test]
		public void LoanBook_when_the_book_to_is_not_existing_then_raise_an_exception()
		{

			stubBooksStorage
			.Setup(
				booksStorage => booksStorage.LoanBook(It.IsAny<long>(), It.IsAny<int>())
			)
			.Throws<DataNotFoundException>();

			Assert.Throws<InvalidOperationException>(() =>
				administrativeBooksLoanService.LoanBook(AnyBookIsbn, AnyRegisteredUser)
			);
		}	
	}
}
