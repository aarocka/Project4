<!-- Kevin To -->
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MakeReservation.aspx.cs" Inherits="RestaurantReviewProject3.MakeReservation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="StyleReviewerRepresentative.css"/>
    <title>Make Reservation</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="body-center">
            <h1>Make your Reservation here!</h1>
            <p>
                <asp:Label ID="Label2" runat="server" Text="Name:" style="font-weight: 700"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </p>
            <p><strong>Pick your date:</strong></p>
            <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
            <br />
            <asp:DropDownList ID="DropDownList1" runat="server" Height="25px" Width="110px" style="font-weight: 700">
                <asp:ListItem Selected="True">Pick a Time</asp:ListItem>
                <asp:ListItem>5:00 PM</asp:ListItem>
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
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" Text="Set Reservation" OnClick="Button1_Click" style="font-weight: 700" />
            <br />
            <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
            <br />
            <br />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Back Home" />
            <br />
        </div>
    </form>
</body>
</html>