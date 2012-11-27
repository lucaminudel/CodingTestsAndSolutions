namespace Bookshelf.DomainModel
{
	public interface IRegisteredUser
	{
		void SendRegisteredUserInfoTo(RegisteredUserInfoReader sendBookInfo);
	}
}