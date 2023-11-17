<!-- Kevin To -->
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RestReviews.aspx.cs" Inherits="RestaurantReviewProject3.RestReviews" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="StyleReviewerRepresentative.css"/>
    <title>Restaurant Reviews</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="body-center">
            <h1>All Reviews for this Restaurant</h1>
            <asp:GridView ID="GridView1" runat="server"></asp:GridView>
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Back Home" style="font-weight: 700" />
        </div>
    </form>
</body>
</html>
