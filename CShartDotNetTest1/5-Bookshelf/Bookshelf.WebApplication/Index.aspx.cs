using System;
using System.Web.UI.WebControls;

namespace Bookshelf.WebApplication
{
	public partial class Index : System.Web.UI.Page
	{
		private IndexModel model;
		private IndexController controller;

		protected void Page_Load(object sender, EventArgs e)
		{
			var modelsAndControllersfactory = new ModelsAndControllersFactory();
			model = modelsAndControllersfactory.CreateIndexModel();
			controller = modelsAndControllersfactory.CreateIndexController(model);
		}


		protected void BooksLoanStatusDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
		{
			controller.ReloadBookshelf();
			e.ObjectInstance = model;
		}

		protected void RegisteredUsersDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
		{
			controller.ReloadRegisteredUsers();
			e.ObjectInstance = model;
		}

		protected void BooksLoanStatusTable_RowCommand(object sender, GridViewCommandEventArgs e)
		{

			controller.ExecuteBookReturn((string)e.CommandName, (string)e.CommandArgument);

			ErrorMessage.Text = model.ErrorMessage;
			ErrorMessage.Visible = string.IsNullOrEmpty(model.ErrorMessage) == false;
			Message.Text = model.Message;
			Message.Visible = string.IsNullOrEmpty(model.Message) == false;

			BooksLoanStatusTable.DataBind();
		}

		protected void RegisteredUsersDropDownList_SelectedIndexChanged(object sender, EventArgs e)
		{
			var dropDownList = (DropDownList)sender;
			string isbn = dropDownList.ValidationGroup;
			string registeredUserId = dropDownList.SelectedValue;
			string registeredUserFullName = dropDownList.SelectedItem.Text;

			controller.ExecuteBookLoan(isbn, registeredUserId, registeredUserFullName);

			ErrorMessage.Text = model.ErrorMessage;
			ErrorMessage.Visible = string.IsNullOrEmpty(model.ErrorMessage) == false;
			Message.Text = model.Message;
			Message.Visible = string.IsNullOrEmpty(model.Message) == false;

			BooksLoanStatusTable.DataBind();
		}


	}
}