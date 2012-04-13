namespace Bookshelf.ApplicationServices
{
	public struct BookLoanStatus
	{
		public long ISBN { get; set; }
		public string Title { get; set; }
		public string Authors { get; set; }
		public bool IsLoaned { get; set; }
	}
}