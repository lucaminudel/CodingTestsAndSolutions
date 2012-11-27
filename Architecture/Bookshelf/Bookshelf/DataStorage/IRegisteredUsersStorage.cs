using System.Collections.Generic;

using Bookshelf.DomainModel;

namespace Bookshelf.DataStorage
{
	public interface IRegisteredUsersStorage
	{
		IList<IRegisteredUser> RetrieveAllUsers();
	}
}