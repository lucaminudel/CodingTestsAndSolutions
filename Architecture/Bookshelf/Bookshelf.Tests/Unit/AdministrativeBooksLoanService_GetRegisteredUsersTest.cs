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
	public class AdministrativeBooksLoanService_GetRegisteredUsersTest
	{
		private IList<ApplicationServices.RegisteredUser> users;
	
		[SetUp]
		public void SetUp()
		{

			var stubRegisteredUsersStorage = new Mock<IRegisteredUsersStorage>(MockBehavior.Loose);

			var administrativeBooksLoanService = new AdministrativeBooksLoanService(
				null, null, null, stubRegisteredUsersStorage.Object);

			stubRegisteredUsersStorage
			.Setup(
				registeredUsersStorage => registeredUsersStorage.RetrieveAllUsers()
			)
			.Returns(new List<IRegisteredUser>()
			{
				new RegisteredUser(9, "Paolo", "Rossi"),
				new RegisteredUser(10, "Roberto", "Baggio"),
				new RegisteredUser(11, "Alex", "Del Piero")
			});

			users = administrativeBooksLoanService.GetRegisteredUsers();
		}

		[Test]
		public void GetRegisteredUsers_return_one_entry_per_available_book()
		{

			Assert.AreEqual(3, users.Count);
		}

		[Test]
		public void GetRegisteredUsers_preserve_the_storage_order()
		{
			Assert.AreEqual(9, users[0].Id);
			Assert.AreEqual(10, users[1].Id);
		}

		[Test]
		public void GetRegisteredUsers_return_the_complete_user_info()
		{
			var firstUser = users[0];

			Assert.AreEqual(9, firstUser.Id);
			Assert.AreEqual("Paolo Rossi", firstUser.FullName);
		}

			
	}
}
