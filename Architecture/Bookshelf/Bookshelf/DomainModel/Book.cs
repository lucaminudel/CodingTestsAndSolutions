namespace Bookshelf.DomainModel
{
	public class Book : IBook
	{
		private readonly long isbn;
		private readonly string title;
		private readonly int authorId;
		private readonly int? loanedToUserId;

		public Book(long isbn, string title, int authorId, int? loanedToUserId)
		{
			this.isbn = isbn;
			this.title = title;
			this.authorId = authorId;
			this.loanedToUserId = loanedToUserId;
		}

		public void SendBookInfoTo(BookInfoReader sendBookInfo)
		{
			sendBookInfo(isbn, title, authorId, loanedToUserId);
		}
	}
}