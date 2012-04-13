using System.Configuration;

using Bookshelf.ApplicationServices;

namespace Bookshelf.WebApplication
{
	public class ModelsAndControllersFactory
	{
		private readonly string connectionString;

		public ModelsAndControllersFactory()
		{
			// Here using some automatic criteria like the host name of the current machine or some
			// manual criteria like an info in the config file it is possible to identify different
			// deploy environments:
			// - development (debug build)
			// - beta_automatic_acceptance_tests (release build)
			// - beta_automatic_unit_tests (release build)
			// - beta_manual_testing (release build)
			// - production (release build)
			// For different environment, different external systems can be wired into the application:
			
			// switch(currentEnvironment) ...
			connectionString = ConfigurationManager.ConnectionStrings["BookshelfDb"].ConnectionString;
		}

		public IndexModel CreateIndexModel()
		{
			return new IndexModel();
		}

		public IndexController CreateIndexController(IndexModel model)
		{
			return new IndexController(model, new AdministrativeBooksLoanServiceFactory(connectionString).Create());
		}

	}
}