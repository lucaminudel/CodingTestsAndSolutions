namespace Bookshelf.DomainModel
{
	public class AuthorFactory : IAuthorFactory
	{
		public IAuthor Create(int id, string firstName, string lastName)
		{
			return new Author(id, firstName, lastName);
		}
	}
}