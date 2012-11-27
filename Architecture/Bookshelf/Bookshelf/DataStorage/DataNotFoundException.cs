using System;
using System.Runtime.Serialization;

namespace Bookshelf.DataStorage
{
	public class DataNotFoundException : Exception
	{
		public DataNotFoundException()
		{
		}

		public DataNotFoundException(string message) : base(message)
		{
		}

		public DataNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}