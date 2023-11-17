<!-- Kevin To -->
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditReservation.aspx.cs" Inherits="RestaurantReviewProject3.EditReservation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit Reservation</title>
    <link rel="stylesheet" href="StyleReviewerRepresentative.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div id="body-center">
            <h1>Edit the Rservations Here!</h1>
            <h2>Change the Name, Date and Time</h2>
            <p>
                <asp:Label ID="Label1" runat="server" Text="Name:" style="font-weight: 700"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </p>
            <p>
                <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
            </p>
            <p>
                <asp:DropDownList ID="DropDownList1" runat="server" style="font-weight: 700">
              
                <asp:ListItem Selected="True">5:00 PM</asp:ListItem>
                <asp:ListItem>5:30 PM</asp:ListItem>
                <asp:ListItem>6:00 PM</asp:ListItem>
                <asp:ListItem>6:30 PM</asp:ListItem>
                <asp:ListItem>7:00 PM</asp:ListItem>
                <asp:ListItem>7:30 PM</asp:ListItem>
                <asp:ListItem>8:00 PM</asp:ListItem>
                <asp:ListItem>8:30 PM</asp:ListItem>
                <asp:ListItem>9:00 PM</asp:ListItem>
                <asp:ListItem></asp:ListItem>
                </asp:DropDownList>
            </p>
            <p>
                <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" style="font-weight: 700" />
            </p>
        </div>
    </form>
</body>
</html>
