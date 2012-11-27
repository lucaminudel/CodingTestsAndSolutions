namespace Bookshelf.DomainModel
{
	public interface IBook
	{
		void SendBookInfoTo(BookInfoReader sendBookInfo);
	}
}