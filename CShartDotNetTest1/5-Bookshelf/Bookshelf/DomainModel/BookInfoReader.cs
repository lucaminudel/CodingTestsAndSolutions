namespace Bookshelf.DomainModel
{
	public delegate void BookInfoReader(long isbn, string title, int authorId, int? loanedToUserId);
}