namespace Bookshelf.DomainModel
{
	public interface IAuthor
	{
		void SendAuthorInfoTo(AuthorInfoReader sendBookInfo);		 
	}
}