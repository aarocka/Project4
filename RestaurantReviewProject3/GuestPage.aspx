<!-- Kevin To -->
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GuestPage.aspx.cs" Inherits="RestaurantReviewProject3.AnonymousPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="StyleReviewerRepresentative.css"/>
    <title>Guest Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="body-center">
            <h1>Welcome!</h1>
            <p>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Logout" style="font-weight: 700" />
            </p>
            <h2>Guest Page</h2>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" DataKeyNames="Id" CssClass="gridview-font">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" InsertVisible="False" ReadOnly="True" Visible="False"/>
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                    <asp:BoundField DataField="Category" HeaderText="Category" SortExpression="Category" />
                    <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                    <asp:ImageField DataImageUrlField="Picture" HeaderText="Picture">
                        <ControlStyle Width="100px" Height="100px" />
                    </asp:ImageField>
                    <asp:TemplateField HeaderText=" See Reviews">
                        <ItemTemplate>
                            <asp:Button runat="server" Text="See Reviews" CommandName="MyButton2" CommandArgument='<%# Eval("Id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText=" Make Reservation">
                        <ItemTemplate>
                            <asp:Button runat="server" Text="Make Reservation" CommandName="MyButton1" CommandArgument='<%# Eval("Id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:fa23_3342_tuk40650ConnectionString %>" SelectCommand="SELECT * FROM [Restaurants]"></asp:SqlDataSource>
        </div>
    </form>
</body>
</html>