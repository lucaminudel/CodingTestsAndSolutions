using System;
using System.Collections.Generic;

using Moq;
using NUnit.Framework;

using Bookshelf.DataStorage;
using Bookshelf.DomainModel;
using Bookshelf.ApplicationServices;

namespace Bookshelf.Tests.Unit
{
	[TestFixture]
	public class AdministrativeBooksLoanService_GetCurrentStatusTest
	{
		private IList<BookLoanStatus> currentBooksLoanStatus;

		[SetUp]
		public void SetUp()
		{
			var firsTuple = new Tuple<IBook, IAuthor>(
				new Book(9780375505669, "The hacker ethic and the spirit of the information age", 12, 300),
				new Author(12, "Pekka", "Himanen"));
			var secondTuple = new Tuple<IBook, IAuthor>(
				new Book(9780596517748, "JavaScript: The Good Parts", 13, null),
				new Author(13, "Douglas", "Crockford"));

			var stubBooksAndAuthorsStorage = new Mock<IBooksAndAuthorsStorage>(MockBehavior.Loose);
			stubBooksAndAuthorsStorage
				.Setup(storage => storage.RetrieveAllBooksWithAuthors())
				.Returns(new List<Tuple<IBook, IAuthor>>
				{
					firsTuple, secondTuple
				});

			var administrativeBooksLoanService = new AdministrativeBooksLoanService(stubBooksAndAuthorsStorage.Object, null, null, null);

			currentBooksLoanStatus = administrativeBooksLoanService.GetCurrentStatus();
		}

		[Test]
		public void GetCurrentStatus_return_one_entry_per_available_book()
		{

			Assert.AreEqual(2, currentBooksLoanStatus.Count);
		}

		[Test]
		public void GetCurrentStatus_preserve_the_storage_order()
		{
			Assert.AreEqual(9780375505669, currentBooksLoanStatus[0].ISBN);
			Assert.AreEqual(9780596517748, currentBooksLoanStatus[1].ISBN);
		}

		[Test]
		public void GetCurrentStatus_return_the_complete_book_info()
		{
			var firstBookStatus = currentBooksLoanStatus[0];

			Assert.AreEqual(9780375505669, firstBookStatus.ISBN);
			Assert.AreEqual("The hacker ethic and the spirit of the information age", firstBookStatus.Title);
			Assert.AreEqual("Pekka Himanen", firstBookStatus.Authors);
			Assert.AreEqual(true, firstBookStatus.IsLoaned);
		}
	
	}
}
