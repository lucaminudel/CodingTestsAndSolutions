using Bookshelf.DomainModel;

namespace Bookshelf.DataStorage
{
	public interface IBooksAndRegisteredUsersStorage
	{
		IRegisteredUser RetrieveTheRegisteredUserThatHaveTheLoanOfTheBook(long isbn);
	}
}