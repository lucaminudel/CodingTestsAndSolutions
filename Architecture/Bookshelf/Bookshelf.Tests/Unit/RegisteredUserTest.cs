using NUnit.Framework;

using Bookshelf.DomainModel;

namespace Bookshelf.Tests.Unit
{
	[TestFixture]
	public class RegisteredUserTest
	{
		[Test]
		public void SendRegisteredUserInfoTo_send_the_expected_info()
		{
			const int registeredUserId = 11;
			const string registeredUserFirstName = "Felipe";
			const string registeredUserLastName = "Massa";
			var registeredUser = new RegisteredUser(registeredUserId, registeredUserFirstName, registeredUserLastName);
			registeredUser.SendRegisteredUserInfoTo((id, firstName, lastName) =>
			{
				Assert.AreEqual(registeredUserId, id);
				Assert.AreEqual(registeredUserFirstName, firstName);
				Assert.AreEqual(registeredUserLastName, lastName);
			});
		}
	}
}
