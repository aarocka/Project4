<!-- Kevin To -->
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditRestrauntInfo.aspx.cs" Inherits="RestaurantReviewProject3.EditRestrauntInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="StyleReviewerRepresentative.css"/>
    <title>Edit Restaurant Information</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="body-center">
            <h1>Edit the Restaurant Information Here!</h1>
            <br />
            <h2>Use the textboxes to change the restaurant info.</h2>
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>:<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>:<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>:<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>:<asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>:<asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" style="font-weight: 700" />
        </div>
    </form>
</body>
</html>
