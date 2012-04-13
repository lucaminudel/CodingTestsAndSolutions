<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Bookshelf.WebApplication.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bookshelf Administration</title>
    <link type="text/css" href="~/site.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
      <div>
        <h1 class="defaultStyle">Bookshelf</h1>
        <h2 class="defaultStyle">Administrative books loan</h2>
        <p class="defaultStyle">
          <asp:Label runat="server" ID="Message" Visible="False" ViewStateMode="Disabled" Mode="Encode" class="messageStyle"/>
          <asp:Label runat="server" ID="ErrorMessage" Visible="False" ViewStateMode="Disabled" Mode="Encode" class="errorStyle"/>
          <asp:GridView ID="BooksLoanStatusTable" runat="server" 
            DataSourceID="BooksLoanStatusDataSource" AutoGenerateColumns="False" 
            CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="defaultStyle" 
            onrowcommand="BooksLoanStatusTable_RowCommand" ShowFooter="True">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
              <asp:BoundField DataField="ISBN" HeaderText="ISBN" SortExpression="ISBN" >
                <HeaderStyle HorizontalAlign="Left" />
              </asp:BoundField>
              <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" >
                <HeaderStyle HorizontalAlign="Left" />
              </asp:BoundField>
              <asp:BoundField DataField="Authors" HeaderText="Authors" SortExpression="Authors" >
                <HeaderStyle HorizontalAlign="Left" />
              </asp:BoundField>
              <asp:TemplateField HeaderText="Status" SortExpression="Status">
                <ItemTemplate>
                  <asp:Label ID="Label1" runat="server" Text='<%# ((bool)Eval("IsLoaned")) ? "Loaned" : null %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField SortExpression="IsLoaned" >
                <ItemTemplate>
                  <asp:LinkButton ID="ReturnButton" runat="server" CssClass="commandStyle" Enabled='<%# ((bool)Eval("IsLoaned")) %>' Visible='<%# ((bool)Eval("IsLoaned")) %>' CommandName="Return" CommandArgument='<%# Eval("ISBN") %>' Text="Return" />
                  <asp:DropDownList runat="server" 
                    ID="RegisteredUsersDropDownList"
                    CssClass="commandStyle"
                    ValidationGroup='<%# Eval("ISBN") %>'
                    Enabled='<%# !(bool)Eval("IsLoaned") %>' 
                    Visible = '<%# !(bool)Eval("IsLoaned") %>'                     
                    DataSourceID="RegisteredUsersDataSource" DataTextField="FullName" 
                    DataValueField="Id" AutoPostBack="True" 
                    onselectedindexchanged="RegisteredUsersDropDownList_SelectedIndexChanged"/>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <FooterTemplate>
                  <asp:LinkButton runat="server" CssClass="defaultStyle" ForeColor="White">Refresh</asp:LinkButton>
                </FooterTemplate>
              </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
          </asp:GridView>
        </p>
        <asp:ObjectDataSource ID="BooksLoanStatusDataSource" runat="server" 
          SelectMethod="GetBookshelf" 
          TypeName="Bookshelf.WebApplication.IndexModel" 
          onobjectcreating="BooksLoanStatusDataSource_ObjectCreating" 
          CacheDuration="1">
        </asp:ObjectDataSource>    
      </div>
      <asp:ObjectDataSource ID="RegisteredUsersDataSource" runat="server" 
        onobjectcreating="RegisteredUsersDataSource_ObjectCreating" 
        TypeName="Bookshelf.WebApplication.IndexModel" 
        SelectMethod="GetRegisteredUsers"></asp:ObjectDataSource>
    </form>
</body>
</html>
