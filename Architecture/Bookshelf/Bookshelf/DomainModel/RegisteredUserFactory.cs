namespace Bookshelf.DomainModel
{
	public class RegisteredUserFactory : IRegisteredUserFactory
	{
		public IRegisteredUser Create(int id, string firstName, string lastName)
		{
			return new RegisteredUser(id, firstName, lastName);
		}
	}
}