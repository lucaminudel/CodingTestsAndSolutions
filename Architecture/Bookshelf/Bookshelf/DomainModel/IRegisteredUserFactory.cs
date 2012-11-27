namespace Bookshelf.DomainModel
{
	public interface IRegisteredUserFactory
	{
		IRegisteredUser Create(int id, string firtsName, string lastName);
	}
}