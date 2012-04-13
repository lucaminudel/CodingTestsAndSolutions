namespace Bookshelf.DomainModel
{
	public class BookFactory : IBookFactory
	{
		public IBook Create(long isbn, string title, int authorId, int? loanedToRegisteredUserId)
		{
			return new Book(isbn, title, authorId, loanedToRegisteredUserId);
		}
	}
}