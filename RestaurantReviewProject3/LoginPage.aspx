<!-- Kevin To -->
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="RestaurantReviewProject3.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="StyleLoginPage.css" />
    <title>Kevin's Restaurant Review App</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="body-center">
            <h1>Kevin's Restaurant Review App</h1>
            <asp:Image ID="Image1" runat="server" Height="106px" ImageUrl="~/pics/KF.png" Width="106px" />
            <br />
            <br />
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" Style="font-weight: 700">
                <asp:ListItem>Representative</asp:ListItem>
                <asp:ListItem>Reviewer</asp:ListItem>
            </asp:RadioButtonList>
            <br />
            <strong>Username:</strong>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br />
            <br />
            <strong>Password:</strong>
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>    
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" Text="Login" OnClick="Button1_Click" Style="font-weight: 700" />
            <br />
            <br />
            <asp:Button ID="Button3" runat="server" Text="Register a User" OnClick="Button3_Click" Style="font-weight: 700" />
            <br />
            <br />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Login as Guest" Style="font-weight: 700" />
            <br />
            <asp:Label ID="Label1" runat="server" Text="Label" Visible="False" ForeColor="Red"></asp:Label>
        </div>
    </form>
</body>
</html>