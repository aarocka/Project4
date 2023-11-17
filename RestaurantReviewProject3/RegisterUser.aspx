<!-- Kevin To -->
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterUser.aspx.cs" Inherits="RestaurantReviewProject3.RegisterUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="StyleReviewerRepresentative.css"/>
    <title>Register an Anccount</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="body-center">
            <h1>Register Here!</h1>
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" style="font-weight: 700" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                 <asp:ListItem>Representative</asp:ListItem>
                 <asp:ListItem>Reviewer</asp:ListItem>
            </asp:RadioButtonList>
            <br />
            <asp:Label ID="Label3" runat="server" Text="Select a restaurant to represent"></asp:Label>
            <br />
            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="Id" Visible="False"></asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:fa23_3342_tuk40650ConnectionString %>" SelectCommand="FindRestWithNoReps" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
            <br />
            <asp:Button ID="Button3" runat="server" Text="Add Restaurant" OnClick="Button3_Click" Visible="False" style="font-weight: 700"/>
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="Name:" style="font-weight: 700"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server" Width="128px"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Username:" style="font-weight: 700"></asp:Label>
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" Text="Register" OnClick="btnShowLabel_Click" AutoPostBack="true" style="font-weight: 700" />
            <br />
           <asp:Label ID="Label4" runat="server" ForeColor="Red" Visible="False" />
            <br />
            <br />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" style="font-weight: 700" Text="Back to Login Page" />
        </div>
    </form>
</body>
</html>