namespace Bookshelf.DomainModel
{
	public interface IBookFactory
	{
		IBook Create(long isbn, string title, int authorId, int? loanedToRegisteredUserId);
	}
}