<!-- Kevin To -->
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReviewerPage.aspx.cs" Inherits="RestaurantReviewProject3.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="ReviewerPageStyle.css"/>
    <title>Reviewer's Page</title>
    <style type="text/css">
        #rating {
            width: 45px;
        }

        #rating0 {
            width: 45px;
        }

        #rating1 {
            width: 45px;
        }

        #rating2 {
            width: 45px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="body-center">
            <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label>
            <h1>Welcome!</h1>
            <p>
            <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Logout" style="font-weight: 700" />
            <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="See your reviews" Width="114px" style="font-weight: 700" />
                <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" style="font-weight: 700" Text="Add a restaurant" Width="129px" />
            </p>
            <h2>Reviewer's Page</h2>
            <p>
            <asp:Label ID="Label1" runat="server" Text="Sort by Categories:"></asp:Label>
            </p>
            <asp:DropDownList ID="DropDownList2" runat="server" DataTextField="Category" DataValueField="Category"></asp:DropDownList>
            <asp:DropDownList ID="DropDownList3" runat="server" DataTextField="Category" DataValueField="Category"></asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:fa23_3342_tuk40650ConnectionString %>" SelectCommand="SELECT DISTINCT [Category] FROM [Restaurants]"></asp:SqlDataSource>
            <br />
            <asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="SearchButton_Click" />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" DataKeyNames="Id" CssClass="gridview-font" Width="600px">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" InsertVisible="False" ReadOnly="True" Visible="False" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                    <asp:BoundField DataField="Category" HeaderText="Category" SortExpression="Category" />
                    <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                    <asp:BoundField DataField="AVG" HeaderText="Average Rating" SortExpression="AVG" />

                    <asp:ImageField DataImageUrlField="Picture" HeaderText="Picture">
                        <ControlStyle Width="100px" Height="100px" />
                    </asp:ImageField>

        <asp:TemplateField>
            <ItemTemplate>
                <asp:Button ID="Button1" runat="server" Text="See Reviews" CommandName="MyButton1" CommandArgument='<%# Eval("Id") %>'/>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Button ID="Button2" runat="server" Text="Make Review" CommandName="MyButton2" CommandArgument='<%# Eval("Id") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Button ID="Button3" runat="server" Text="Make Reservations" CommandName="MyButton3" CommandArgument='<%# Eval("Id") %>'/>
            </ItemTemplate>
        </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:fa23_3342_tuk40650ConnectionString %>" SelectCommand="GetRestaurantAverages" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
