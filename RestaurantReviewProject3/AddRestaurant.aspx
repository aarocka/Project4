<!-- Kevin To -->
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddRestaurant.aspx.cs" Inherits="RestaurantReviewProject3.AddRestaurant" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Restaurant</title>
    <link rel="stylesheet" href="StyleReviewerRepresentative.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div id="body-center">
            <h1>Add your Restaurant Here!</h1>
            <asp:Label ID="Label1" runat="server" Text="Name:" style="font-weight: 700"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="Label2" runat="server" Text="Address:" style="font-weight: 700"></asp:Label>
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="Label3" runat="server" Text="Category:" style="font-weight: 700"></asp:Label>
            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="Label4" runat="server" Text="Phone:" style="font-weight: 700"></asp:Label>
            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="Label5" runat="server" Text="Picture:" style="font-weight: 700"></asp:Label>
            <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="Label6" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
            <br />
            <asp:Button ID="Button1" runat="server" Text="Add Restaurant" style="font-weight: 700" OnClick="Button1_Click" />
            <br />
            <br />
            <asp:Button ID="Button2" runat="server" Text="Back Home" OnClick="Button2_Click" style="font-weight: 700" />
        </div>
    </form>
</body>
</html>