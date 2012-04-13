namespace Bookshelf.DomainModel
{
	public class RegisteredUser : IRegisteredUser
	{
		private readonly int id;
		private readonly string firstName;
		private readonly string lastName;

		public RegisteredUser(int id, string firstName, string lastName)
		{
			this.id = id;
			this.firstName = firstName;
			this.lastName = lastName;
		}

		public void SendRegisteredUserInfoTo(RegisteredUserInfoReader sendRegisteredUserInfo)
		{
			sendRegisteredUserInfo(id , firstName, lastName);
		}
	}
}