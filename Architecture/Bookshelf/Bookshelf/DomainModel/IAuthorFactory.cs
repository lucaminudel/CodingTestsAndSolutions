namespace Bookshelf.DomainModel
{
	public interface IAuthorFactory
	{
		IAuthor Create(int id, string firstName, string lastName);
	}
}