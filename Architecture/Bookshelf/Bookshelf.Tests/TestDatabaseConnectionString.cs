namespace Bookshelf.Tests
{
	public class TestDatabaseConnectionString
	{
		private const string ConnectionString = "Data Source=(local);Initial Catalog=TMP;Integrated Security=True";

		public static implicit operator string(TestDatabaseConnectionString value)
		{
			return ConnectionString;
		}
	}
}
