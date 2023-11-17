<!-- Kevin To -->
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RepresentativePage.aspx.cs" Inherits="RestaurantReviewProject3.RepresentativePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="StyleReviewerRepresentative.css" />
    <title>Representative Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="body-center">
            <h1>Welcome!</h1>
            <h2>Representative Page</h2>
            <p>
                <asp:Button ID="Button1" runat="server" Text="Logout" Style="font-weight: 700" OnClick="Button1_Click" />
                <asp:Button ID="Button3" runat="server" Text="Make Reservation" style="font-weight: 700" OnClick="Button3_Click" />
                <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Edit Restaurant" style="font-weight: 700" />
            </p>
            <br />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" DataKeyNames="Id" CssClass="gridview-font" Width="632px" >
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" InsertVisible="False" ReadOnly="True" Visible="False" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                    <asp:BoundField DataField="Time" HeaderText="Time" SortExpression="Time" />
                    <asp:BoundField DataField="Restaurant" HeaderText="Restaurant" SortExpression="Restaurant" />
                     <asp:TemplateField HeaderText=" Edit Reservation">
                        <ItemTemplate>
                            <asp:Button runat="server" Text="Edit Reservations" CommandName="MyButton2" CommandArgument='<%# Eval("Id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText=" Delete Reservation">
                        <ItemTemplate>
                            <asp:Button runat="server" Text="Delete Reservation" CommandName="MyButton1" CommandArgument='<%# Eval("Id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
