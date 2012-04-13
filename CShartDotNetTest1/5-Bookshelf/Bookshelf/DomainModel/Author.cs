namespace Bookshelf.DomainModel
{
	public class Author : IAuthor
	{
		private readonly int id;
		private readonly string firstName;
		private readonly string lastName;

		public Author(int id, string firstName, string lastName)
		{
			this.id = id;
			this.firstName = firstName;
			this.lastName = lastName;
		}

		public void SendAuthorInfoTo(AuthorInfoReader sendBookInfo)
		{
			sendBookInfo(id, firstName, lastName);
		}
	}
}