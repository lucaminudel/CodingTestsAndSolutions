using System.Collections.Generic;

using BookAndAuthorTupla = System.Tuple<Bookshelf.DomainModel.IBook, Bookshelf.DomainModel.IAuthor>;

namespace Bookshelf.DataStorage
{
	public interface IBooksAndAuthorsStorage
	{
		IList<BookAndAuthorTupla> RetrieveAllBooksWithAuthors();
	}
}