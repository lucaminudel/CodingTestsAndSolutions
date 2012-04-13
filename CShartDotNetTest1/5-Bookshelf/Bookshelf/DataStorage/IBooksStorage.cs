namespace Bookshelf.DataStorage
{
	public interface IBooksStorage
	{
		void ReturnBook(long isbn, int fromRegisteredUserId);
		void LoanBook(long bookIsbn, int byRegisteredUserId);
	}
}